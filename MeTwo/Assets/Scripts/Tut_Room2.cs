using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Room2 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Tile tile1;
    [SerializeField] private Tile tile2;
    private bool doorOpening;

    void Update()
    {
        if(tile1.tilePushed && tile2.tilePushed)
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
        door.transform.Translate(Vector3.up * Time.deltaTime * 5);
    }
}
