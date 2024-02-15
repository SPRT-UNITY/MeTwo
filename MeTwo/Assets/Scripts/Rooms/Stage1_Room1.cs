using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Room1 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Btn btn1;
    [SerializeField] private Btn btn2;
    [SerializeField] private Btn btn3;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    
    [SerializeField] private GameObject button3;
    
    private Animator anim;
    
    private void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    void Update()
    {
        if(btn1.btnPushed && button1 != null)
        {
            Destroy(button1, 1.5f);
            button2.SetActive(true);
        }
        if(btn2.btnPushed && button2 != null)
        {
            Destroy(button2, 1.5f);
            button3.SetActive(true);
        }
        if(btn3.btnPushed)
        {
            DoorOpen();
        }
    }

    public void DoorOpen()
    {
        anim.SetBool("IsOpen", true);
    }
}
