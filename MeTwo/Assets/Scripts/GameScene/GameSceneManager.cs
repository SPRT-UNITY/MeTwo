using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    PlayerManager playerManager;
    GameObject stageObject;

    public Stage stage { get; private set; }

    PlayerController playerController;
    PlayerController shadowController;

    public event Action OnClearGameEvent;

    float gameTime;

    private void Awake()
    {
        playerManager = GetComponentInChildren<PlayerManager>();
        if(playerManager == null) 
        {
            GameObject prefab = Resources.Load("Prefabs/PlayerManager") as GameObject;
            GameObject playerManagerObject = Instantiate(prefab);
            playerManagerObject.transform.parent = transform;
            playerManager = playerManagerObject.GetComponent<PlayerManager>();
        }
    }

    void Start()
    {
        // 플레이어에 빙의
        playerManager.Initialize(playerController, shadowController);

        // 카메라가 플레이어에 포커스
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void InitGame() 
    {
        stageObject = StageSelector.Instance.loadStage();
        stage = stageObject.GetComponent<Stage>();

        GameObject prefab = Resources.Load("Prefabs/Player") as GameObject;

        GameObject playerObject = Instantiate(prefab, stage.playerStarter.transform.position, stage.playerStarter.transform.rotation);
        playerController = playerObject.GetComponent<PlayerController>();

        GameObject shadowObject = Instantiate(prefab, stage.shadowStarter.transform.position, stage.shadowStarter.transform.rotation);
        shadowController = shadowObject.GetComponent<PlayerController>();

        SetGamePlaying();
    }

    public void ClearGame() 
    {
        OnClearGameEvent.Invoke();
        stage.clearTime = gameTime;
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
        Time.fixedDeltaTime = 0.02f;
    }

    public void RespawnPlayer() 
    {
        playerController.gameObject.transform.position = stage.playerStarter.transform.position + Vector3.up * 0.5f;
    }

    public void RespawnShadow() 
    {
        shadowController.gameObject.transform.position = stage.playerStarter.transform.position + Vector3.up * 0.5f;
    }
}
