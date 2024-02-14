using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _canEnter { get; set; } // 입장 가능 스테이지
    private int _nowEnter { get; set; } // 현재 입장 스테이지

    private float _scoreFloor0 { get; set; } // 0스테이지 점수
    private float _scoreFloor1 { get; set; } // 1스테이지 점수
    private float _scoreFloor2 { get; set; } // 2스테이지 점수 

    // ▲ 일단 이 정도?

    public event Func<bool> OnSetCanEnter;
    public event Func<bool> OnSetNowEnter;
    public event Func<bool> OnSetScore;

    private void Awake()
    {
        _canEnter = PlayerPrefs.GetInt("CanEnter", 0);
        _nowEnter = -1; // 타이틀화면: -1
        _scoreFloor0 = PlayerPrefs.GetFloat("ScoreFloor0", 0.0f);
        _scoreFloor1 = PlayerPrefs.GetFloat("ScoreFloor1", 0.0f);
        _scoreFloor2 = PlayerPrefs.GetFloat("ScoreFloor2", 0.0f);
    }

    public int canEnter
    {
        get { return _canEnter; }
        set
        {
            _canEnter = value;
            PlayerPrefs.SetInt("CanEnter", _canEnter);
            OnSetCanEnter?.Invoke();
        }
    }
    public int nowEnter
    {
        get { return _nowEnter; }
        set
        {
            _nowEnter = value;
            OnSetNowEnter?.Invoke();
        }
    }
    public float scoreFloor0
    {
        get { return _scoreFloor0; }
        set
        {
            _scoreFloor0 = value;
            PlayerPrefs.SetFloat("ScoreFloor0", _scoreFloor0);
            OnSetScore?.Invoke();
        }
    }
    public float scoreFloor1
    {
        get { return _scoreFloor1; }
        set
        {
            _scoreFloor1 = value;
            PlayerPrefs.SetFloat("ScoreFloor1", _scoreFloor1);
            OnSetScore?.Invoke();
        }
    }
    public float scoreFloor2
    {
        get { return _scoreFloor2; }
        set
        {
            _scoreFloor2 = value;
            PlayerPrefs.SetFloat("ScoreFloor2", _scoreFloor2);
            OnSetScore?.Invoke();
        }
    }

    // 랭크 매기기 메서드 등?
    // ...
}
