using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Action<InputAction.CallbackContext> onMoveEvent;
    public Action<Vector2> onLookEvent;
    public Action onJumpEvent;
    public Action onInteractEvent;
    public Action onLeftRotateEvent;
    public Action onRightRotateEvent;
    
    [Header("Movement")]
    public float moveSpeed = 200f;
    public float jumpForce = 50f;
    public float rotateSpeed = 0.1f;
    public LayerMask groundLayerMask;
    private bool _isMoving;
    private bool _isGround;
    private Vector2 _curMovementInput;
    private Rigidbody _rigidbody;
    
    [Header("Look")]
    public Transform cameraHorizontalFocus;
    public Transform cameraVerticalFocus;
    public Transform character;
    public float minXLook = -60;
    public float maxXLook = 30;
    public float lookSensitivity = 2f;
    public LayerMask interactableLayerMask;
    private float _camCurXRot;

    private Vector2 _mouseDelta;
    
    private Animator _animator;
    private GameObject _virtualCamera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        //_virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
    }

    private void OnEnable()
    {
        onMoveEvent += OnMoveInput;
        //onLookEvent += OnLookInput;
        onJumpEvent += OnJumpInput;
        onInteractEvent += OnInteractionInput;
        onLeftRotateEvent += OnLeftRotateInput;
        onRightRotateEvent += OnRightRotateInput;
    }

    private void Update()
    {
        CheckIsGround();
        CheckInteraction();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void LateUpdate()
    {
        if(_isMoving)
            CharacterLook();
    }
    
    private void Move()
    {
        var dir = cameraHorizontalFocus.forward * _curMovementInput.y + cameraHorizontalFocus.right * _curMovementInput.x;
        dir *= moveSpeed * Time.deltaTime;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    // 쿼터 뷰 변경으로 사용 X
    private void CameraLook()
    {
        _camCurXRot += _mouseDelta.y * lookSensitivity * Time.deltaTime;
        _camCurXRot = Mathf.Clamp(_camCurXRot, minXLook, maxXLook);
        // 플레이어의 cameraVerticalFocus를 돌려 카메라 수직 회전.
        // cinemachine의 virtual카메라의 aim이 cameraVerticalFocus여서 카메라도 회전함.
        cameraVerticalFocus.localEulerAngles = new Vector3(-_camCurXRot, 0, 0);

        // 플레이어의 cameraVerticalFocus를 돌려 카메라 수평 회전.
        cameraHorizontalFocus.eulerAngles += new Vector3(0, _mouseDelta.x * lookSensitivity * Time.deltaTime);

        // 움직일 때만 캐릭터 방향 조정
        if (_isMoving)
            CharacterLook();
    }
    
    // 캐릭터 보는 방향 설정
    private void CharacterLook()
    {
        var targetRotation = cameraHorizontalFocus.eulerAngles;

        if (_curMovementInput.x != 0)
        {
            var angle = _curMovementInput.y != 0 ? (_curMovementInput.y > 0 ? 45f : 135f) : 90f;
            targetRotation.y += _curMovementInput.x < 0 ? -angle : angle;
        }
        else
        {
            if (_curMovementInput.y < 0)
                targetRotation.y -= 180f;
        }

        character.rotation = Quaternion.Slerp(character.rotation, Quaternion.Euler(targetRotation), rotateSpeed);
    }

    // 캐릭터 간의 카메라 이동을 위한 Method - 쿼터 뷰 변경으로 사용 X
    public void ToggleVirtualCamera(bool active)
    {
        _virtualCamera.SetActive(active);
    }

    // 이동하다가 캐릭터 변경시 움직이는 것 방지용
    public void StopMove()
    {
        _isMoving = false;
        _animator.SetBool("IsRunning", _isMoving);
        _curMovementInput = Vector2.zero;
        //_mouseDelta = Vector2.zero;
        //ResetCamera();
    }

    public void ResetCamera()
    {
        cameraHorizontalFocus.eulerAngles = character.eulerAngles;
    }
    
    // 이동설정
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                _isMoving = true;
                _animator.SetBool("IsRunning", _isMoving);
                _curMovementInput = context.ReadValue<Vector2>();
                return;
            case InputActionPhase.Canceled:
                _isMoving = false;
                _animator.SetBool("IsRunning", _isMoving);
                _curMovementInput = Vector2.zero;
                return;
        }
    }
    
    // 마우스 입력 - 쿼터뷰 변경으로 사용 X
    private void OnLookInput(Vector2 mouseInput)
    {
        _mouseDelta = mouseInput;
    }

    // 점프
    private void OnJumpInput()
    {
        if (!_isGround) return;
        
        _animator.SetTrigger("Jump");
        // 슈퍼 점프 방지
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }

    private void OnLeftRotateInput()
    {
        cameraHorizontalFocus.Rotate(new Vector3(0, 90f, 0));
    }
    
    private void OnRightRotateInput()
    {
        cameraHorizontalFocus.Rotate(new Vector3(0, -90f, 0));
    }

    // 바닥 체크
    private void CheckIsGround()
    {
        var rays = new[]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        _isGround = rays.Any(ray => Physics.Raycast(ray, 0.1f, groundLayerMask));
        _animator.SetBool("IsGround", _isGround);
    }

    // 상호작용 UI 띄우는 Method
    private GameObject _latestGameObject;
    private UI_Interact _uiInteract;
    private void CheckInteraction()
    {
        var ray = new Ray(transform.position + (Vector3.up * 0.5f), character.forward);

        if (Physics.Raycast(ray, out var hit, 1f, interactableLayerMask))
        {
            if (_latestGameObject != null && _latestGameObject == hit.transform.gameObject)
                return;

            _latestGameObject = hit.transform.gameObject;
            
            _uiInteract = TempManagers.UI.ShowDisplayUI<UI_Interact>(messages: new string[] { "F", "상호작용하기" });
        }
        else
        {
            _latestGameObject = null;
            if (_uiInteract != null)
            {
                TempManagers.UI.CloseDisplayUI(_uiInteract.gameObject.name);
                _uiInteract = null;
            }
        }
    }
    
    // 상호작용 시도
    private void OnInteractionInput()
    {
        var ray = new Ray(transform.position + (Vector3.up * 0.5f), character.forward);
        
        if (Physics.Raycast(ray, out var hit, 1f, interactableLayerMask))
        {
            hit.collider.GetComponent<Btn>().PushButton();
        }
    }
    
    // 레이 확인용 Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down * 0.1f);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down * 0.1f);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down * 0.1f);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down * 0.1f);
        
        Gizmos.DrawRay(transform.position + (Vector3.up * 0.5f), character.forward * 0.5f);
    }
}
