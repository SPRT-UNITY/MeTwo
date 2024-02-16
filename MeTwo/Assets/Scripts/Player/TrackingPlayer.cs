using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _shadowTransform;
    private bool _isBoth;
    private bool _isPlayer;

    public float movingSpeed = 5f;

    private void Start()
    {
        Initialize();
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void Initialize()
    {
        _playerTransform = PlayerManager.Instance.playerController.transform;
        _shadowTransform = PlayerManager.Instance.shadowController.transform;
        _isBoth = PlayerManager.Instance.isBoth;
        _isPlayer = true;
        
        PlayerManager.Instance.onBothCharacterControlEvent += CheckBothControl;
        PlayerManager.Instance.onSwapCharacterEvent += CheckSwap;
    }

    private void UpdatePosition()
    {
        Vector3 targetPos;
        
        if (_isBoth)
            targetPos = (_playerTransform.position + _shadowTransform.position) / 2;
        else if (_isPlayer)
            targetPos = _playerTransform.position;
        else
            targetPos = _shadowTransform.position;

        transform.position = targetPos;
    }
    
    private void CheckBothControl()
    {
        _isBoth = !_isBoth;
    }

    private void CheckSwap()
    {
        _isPlayer = !_isPlayer;
    }
}
