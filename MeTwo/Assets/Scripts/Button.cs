using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Door;
    public bool doorIsOpening = true;
    public bool btnPushed;
    private Animator btnAnim;
    private Animator doorAnim;

    private void Start() 
    {
        btnAnim = transform.GetComponent<Animator>();
        doorAnim = Door.GetComponent<Animator>();
    }


    void Update()
    {
        if(btnPushed)
        {
            btnAnim.SetBool("IsPush", true);
            if (doorIsOpening)
            {
                doorAnim.SetBool("IsOpen", true);
            }
        }
        
    }

    public void PushButton() 
    {
        btnPushed = !btnPushed;
    }
}
