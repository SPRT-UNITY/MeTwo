using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Action<InputAction.CallbackContext> onMoveEvent;
    public Action<Vector2> onLookEvent;
    public Action onJumpEvent;
    public Action onInteractEvent;
    public Action onBothCharacterControlEvent;
    public Action onSwapCharacterEvent;
    public Action onLeftRotateEvent;
    public Action onRightRotateEvent;
    
    private PlayerController _playerController;
    private PlayerController _shadowController;

    // 현재 메인으로 조종하는 캐릭터
    private PlayerController _currentPlayerController;
    // 동시 조종 중 체크
    private bool _isBoth;

    private void Awake()
    {
        onBothCharacterControlEvent += ToggleBothCharacterControl;
        onSwapCharacterEvent += SwapCharacter;
    }

    private void Start()
    {

    }

    public void Initialize(PlayerController playerController, PlayerController shadowController)
    {
        _playerController = playerController;
        // 방향 전환 등록
        onLeftRotateEvent += _playerController.onLeftRotateEvent;
        onRightRotateEvent += _playerController.onRightRotateEvent;
        
        _shadowController = shadowController;
        // 방향 전환 등록
        onLeftRotateEvent += _shadowController.onLeftRotateEvent;
        onRightRotateEvent += _shadowController.onRightRotateEvent;
        
        // 메인 캐릭터 설정
        _currentPlayerController = _playerController;
        _isBoth = false;
        
        SubscribePlayerAction();
    }

    // 동시조종 Method
    private void ToggleBothCharacterControl()
    {
        if (_isBoth)
        {
            // 동시 조종 해제
            _isBoth = false;
            UnsubscribePlayerAction(true);
        }
        else
        {
            // 분신이 생성되어 있는지 체크 후 생성
            // CheckShadowActive();
            
            // 동시 조종 시작
            _isBoth = true;
            SubscribePlayerAction(true);
        }
    }

    // 분신과 위치 변경
    private void SwapCharacter()
    {
        // 분신이 생성되어 있는지 체크 후 생성
        // CheckShadowActive();
        
        // 동시 조종 중이면 remove하지 않음
        if (!_isBoth)
            UnsubscribePlayerAction();
        
        // 카메라 조정 및 메인 캐릭터 변경
        //ToggleVirtualCamera(false);
        //_currentPlayerController.ResetCamera();
        _currentPlayerController = _currentPlayerController == _playerController ? _shadowController : _playerController;
        //ToggleVirtualCamera(true);
        
        // 동시 조종 중이면 Add하지 않음
        if (!_isBoth)
            SubscribePlayerAction();
    }

    /*
    // 분신이 활성화인지 확인 - 배치식으로 변경 - 사용 X
    private void CheckShadowActive()
    {
        var characterControl = _currentPlayerController == _playerController ? _shadowController : _playerController;

        
        // 비활성화일시 현재 캐릭터 앞에 소환
        if (!characterControl.gameObject.activeSelf)
        {
            SummonShadow(characterControl);
            //characterControl.gameObject.SetActive(true);
        }
    }

    // 현재 캐릭터 앞에 소환 - 배치식으로 변경 - 사용 X
    private void SummonShadow(PlayerController playerController)
    {
        if (!_currentPlayerController.CheckFrontForSummon())
            return;
        
        //var characterControl = _currentCharacterControl == _character1Control ? _character2Control : _character1Control;

        playerController.transform.position = _currentPlayerController.GetFrontPosition();
        playerController.character.eulerAngles = _currentPlayerController.GetRotation();
        
        playerController.gameObject.SetActive(true);
    }*/

    // Action 구독
    private void SubscribePlayerAction(bool isBoth = false)
    {
        PlayerController playerController;
        if (isBoth)
            playerController = _currentPlayerController == _playerController ? _shadowController : _playerController;
        else
            playerController = _currentPlayerController;
        
        onMoveEvent += playerController.onMoveEvent;
        //onLookEvent += playerController.onLookEvent;
        onJumpEvent += playerController.onJumpEvent;
        onInteractEvent += playerController.onInteractEvent;
    }

    // Action 구독 해제
    private void UnsubscribePlayerAction(bool isBoth = false)
    {
        PlayerController playerController;
        if (isBoth)
            playerController = _currentPlayerController == _playerController ? _shadowController : _playerController;
        else
            playerController = _currentPlayerController;
        
        onMoveEvent -= playerController.onMoveEvent;
        //onLookEvent -= playerController.onLookEvent;
        onJumpEvent -= playerController.onJumpEvent;
        onInteractEvent -= playerController.onInteractEvent;
        playerController.StopMove();
    }

    /*카메라 전환 - 쿼터뷰로 변경 - 사용X
    private void ToggleVirtualCamera(bool active, bool other = false)
    {
        PlayerController playerController;
        if (other)
            playerController = _currentPlayerController == _playerController ? _shadowController : _playerController;
        else
            playerController = _currentPlayerController;
        
        playerController.ToggleVirtualCamera(active);
    }*/

    public void CallMoveEvent(InputAction.CallbackContext context)
    {
        onMoveEvent?.Invoke(context);
    }

    // 쿼터뷰 변경으로 인해 사용 X
    public void CallLookEvent(InputAction.CallbackContext context)
    {
        onLookEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void CallJumpEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onJumpEvent?.Invoke();
    }

    public void CallInteractionEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onInteractEvent?.Invoke();
    }

    public void CallBothCharacterControlEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onBothCharacterControlEvent?.Invoke();
    }
    
    public void CallSwapCharacterEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onSwapCharacterEvent?.Invoke();
    }

    public void CallLeftRotateEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onLeftRotateEvent?.Invoke();
    }
    
    public void CallRightRotateEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onRightRotateEvent?.Invoke();
    }
    public void CallPauseEvent(InputAction.CallbackContext context) // 왠지 로그가 세개씩 뜨긴 하는데 아무렴 어떤가
    {
        //Debug.Log($"CallPauseEvent 작동함, 현재 PopupStack수 : {TempManagers.UI.GetPopStackCount()}");
        if (context.phase == InputActionPhase.Started)
        {
            if (TempManagers.UI.GetPopStackCount() > 0)
            {
                TempManagers.UI.ClosePopupUI();
                if (TempManagers.UI.GetPopStackCount() == 0)
                    TempManagers.SetStatePlaying();
            }
            else
            {
                TempManagers.UI.ShowPopupUI<UI_Pause>();
                TempManagers.SetStatePause();
            }
        }
    }
}
