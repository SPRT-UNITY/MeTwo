using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Action onCameraLeftRotationEvent;
    public Action onCameraRightRotationEvent;

    public GameObject quarterViewPrefab;
    public List<GameObject> quarterViewCameras;
    public TrackingPlayer cameraFocus;
    private int _currentCamera;

    private void Awake()
    {
        onCameraLeftRotationEvent += LeftRotateCamera;
        onCameraRightRotationEvent += RightRotateCamera;
        _currentCamera = 0;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var go = Instantiate(quarterViewPrefab);
        quarterViewCameras = go.GetComponentsInChildren<CinemachineVirtualCamera>().Select(o => o.gameObject).ToList();
        
        for (var i = 0; i < quarterViewCameras.Count; i++)
        {
            quarterViewCameras[i].SetActive(i == _currentCamera);
        }
    }
    
    private void LeftRotateCamera()
    {
        if(quarterViewCameras.Count == 0)
            return;
        
        quarterViewCameras[_currentCamera].SetActive(false);
        _currentCamera = (_currentCamera - 1) % quarterViewCameras.Count;
        if (_currentCamera < 0)
            _currentCamera = quarterViewCameras.Count - 1;
        
        quarterViewCameras[_currentCamera].SetActive(true);
    }

    private void RightRotateCamera()
    {
        if(quarterViewCameras.Count == 0)
            return;
        
        quarterViewCameras[_currentCamera].SetActive(false);
        
        _currentCamera = (_currentCamera + 1) % quarterViewCameras.Count;
        
        quarterViewCameras[_currentCamera].SetActive(true);
    }

    public void CallCameraLeftRotationEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onCameraLeftRotationEvent?.Invoke();
        }
    }
    
    public void CallCameraRightRotationEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onCameraRightRotationEvent?.Invoke();
        }
    }
}
