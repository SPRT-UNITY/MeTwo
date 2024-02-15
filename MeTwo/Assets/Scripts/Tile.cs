using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Animator tileAnim;
    public bool tilePushed;

    private void Start()
    {
        tileAnim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if(tilePushed)
        {
            tileAnim.SetBool("IsPush", true);
        }
        else
        {
            tileAnim.SetBool("IsPush", false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            tilePushed = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            tilePushed = false;
        }
    }
}
