using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = GameObject.Find("GameSceneManager");

                if (gameObject == null)
                {
                    gameObject = new GameObject("GameSceneManager");
                    instance = gameObject.AddComponent<GameSceneManager>();
                }
            }
            return instance;
        }
    }

    GameObject stageObject;
    Stage stage;

    public event Action OnClearGameEvent;

    float gameTime;

    private void Awake()
    {
        
    }

    void Start()
    {
        // 플레이어에 빙의
        // 카메라가 플레이어에 포커스
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void InitGame() 
    {
        stageObject = StageSelector.Instance.loadStage();
        stage = stage.GetComponent<Stage>();

        // 플레이어 생성
        // stage.playerStarter.transform.position

        // 분신 생성
        // stage.shadowStarter.transform.position

        SetGamePlaying();
    }

    public void ClearGame() 
    {
        OnClearGameEvent.Invoke();
    }

    public void SetGamePause() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = float.MaxValue;
    }

    public void SetGamePlaying() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedTime;
    }
}
