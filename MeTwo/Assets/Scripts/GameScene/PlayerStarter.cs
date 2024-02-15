using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerFlag 
{
    Player, Shadow
}

public class PlayerStarter : MonoBehaviour
{
    public PlayerFlag playerFlag;

    private void OnDrawGizmos()
    {
        Gizmos.color = playerFlag == PlayerFlag.Player ? Color.green : Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

    public Vector3 GetSpawnPosition() 
    {
        return transform.position;
    }
}
