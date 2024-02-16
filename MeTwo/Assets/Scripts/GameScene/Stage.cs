using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public PlayerStarter playerStarter;
    public PlayerStarter shadowStarter;
    public ClearObject clearObject;
    public AudioClip stageBGM;

    private float _clearTime;

    public float clearTime
    {
        get { return _clearTime; }
        set 
        { 
            _clearTime = value;
            Debug.Log(transform.root.name);
            PlayerPrefs.SetFloat(transform.root.name, _clearTime);
        } 
    }

    private void Awake()
    {
        PlayerStarter[] starters = transform.GetComponentsInChildren<PlayerStarter>();
        foreach(var i in starters) 
        {
            if(i.playerFlag == PlayerFlag.Player)
                playerStarter = i;
            else
                shadowStarter = i;
        }

        clearObject = transform.GetComponentInChildren<ClearObject>();

        if(playerStarter == null || shadowStarter == null) 
        {
            Debug.LogError("playerStarter 혹은 ShadowStarter가 없습니다!");
        }

        if(clearObject == null) 
        {
            Debug.LogError("clearObject가 없습니다!");
        }

        clearTime = PlayerPrefs.GetFloat(transform.root.name, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        PlayerStarter[] starters = transform.GetComponentsInChildren<PlayerStarter>();
        foreach (var i in starters)
        {
            if (i.playerFlag == PlayerFlag.Player)
                playerStarter = i;
            else
                shadowStarter = i;
        }

        clearObject = transform.GetComponentInChildren<ClearObject>();

        if (playerStarter == null || shadowStarter == null)
        {
            Debug.LogError("playerStarter 혹은 ShadowStarter가 없습니다!");
        }

        if (clearObject == null)
        {
            Debug.LogError("clearObject가 없습니다!");
        }
    }
}
