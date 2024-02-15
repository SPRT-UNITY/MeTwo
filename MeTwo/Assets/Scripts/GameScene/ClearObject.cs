using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClearObject : MonoBehaviour
{
    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        boxCollider = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position, boxCollider.size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            GameSceneManager.Instance.ClearGame();
        }
    }
}
