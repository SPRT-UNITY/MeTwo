using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public PlayerStarter playerStarter;
    public PlayerStarter shadowStarter;
    public ClearObject clearObject;

    private void Awake()
    {
        PlayerStarter[] starters = GameObject.FindObjectsOfType<PlayerStarter>();
        foreach(var i in starters) 
        {
            if(i.playerFlag == PlayerFlag.Player)
                playerStarter = i;
            else
                shadowStarter = i;
        }

        clearObject = GameObject.FindObjectOfType<ClearObject>();

        if(playerStarter == null || shadowStarter == null) 
        {
            Debug.LogError("playerStarter 혹은 ShadowStarter가 없습니다!");
        }

        if(clearObject == null) 
        {
            Debug.LogError("clearObject가 없습니다!");
        }
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
        PlayerStarter[] starters = GameObject.FindObjectsOfType<PlayerStarter>();
        foreach (var i in starters)
        {
            if (i.playerFlag == PlayerFlag.Player)
                playerStarter = i;
            else
                shadowStarter = i;
        }

        clearObject = GameObject.FindObjectOfType<ClearObject>();

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
