using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Btn btn1;
    [SerializeField] private Btn btn2;
    private Animator anim;
    
    private void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    void Update()
    {
        if(btn1.btnPushed && btn2.btnPushed)
        {
            DoorOpen();
        }
    }

    public void DoorOpen()
    {
        anim.SetBool("IsOpen", true);
    }
}