using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraDirection
{
    North = 0,
    East,
    West,
    South,
}

public class CameraMovement : MonoBehaviour
{
    public Action onCameraLeftRotationEvent;
    public Action onCameraRightRotationEvent;

    public List<GameObject> quarterViewCameras;
    public CameraDirection currentCameraDirection;
    private int _currentCamera;

    private void Awake()
    {
        onCameraLeftRotationEvent += LeftRotateCamera;
        onCameraRightRotationEvent += RightRotateCamera;
        _currentCamera = 0;
        currentCameraDirection = (CameraDirection)_currentCamera;
    }

    public void Initialize(List<GameObject> camerasInfo)
    {
        // 기존 카메라 비활성화
        if(quarterViewCameras.Count != 0)
            quarterViewCameras[_currentCamera].SetActive(false);
        
        quarterViewCameras = camerasInfo;
        if(quarterViewCameras.Count != 0)
            quarterViewCameras[_currentCamera].SetActive(true);
    }
    
    private void LeftRotateCamera()
    {
        if(quarterViewCameras.Count == 0)
            return;
        
        quarterViewCameras[_currentCamera].SetActive(false);
        _currentCamera = (_currentCamera - 1) % quarterViewCameras.Count;
        if (_currentCamera < 0)
            _currentCamera = quarterViewCameras.Count - 1;

        currentCameraDirection = (CameraDirection)_currentCamera;
        
        quarterViewCameras[_currentCamera].SetActive(true);
    }

    private void RightRotateCamera()
    {
        if(quarterViewCameras.Count == 0)
            return;
        
        quarterViewCameras[_currentCamera].SetActive(false);
        
        _currentCamera = (_currentCamera + 1) % quarterViewCameras.Count;
        currentCameraDirection = (CameraDirection)_currentCamera;
        
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
