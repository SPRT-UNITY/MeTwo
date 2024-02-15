using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Room1 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Btn btn;
    private bool doorOpening;

    void Update()
    {
        if(btn.btnPushed)
        {
            if(door.transform.position.y > 17f)
            {
                doorOpening = false;
            }
            else
            {
                doorOpening = true;
            }
        }
        if(doorOpening)
        {
            DoorOpen();
        }
        if(door.transform.position.y > 17f)
        {
            doorOpening = false;
        }
    }

    public void DoorOpen()
    {
        Debug.Log(door.transform.position.y);
        door.transform.Translate(Vector3.up * Time.deltaTime * 5);
    }
}
