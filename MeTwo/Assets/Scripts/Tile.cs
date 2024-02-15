using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Animator tileAnim;
    public bool tilePushed;
    public bool tilePushing;

    private void Start()
    {
        tileAnim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if(tilePushing)
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
            tilePushing = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            tilePushing = false;
        }
    }
}
