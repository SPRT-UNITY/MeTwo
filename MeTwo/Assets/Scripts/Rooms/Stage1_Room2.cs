using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Room2 : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Tile tile1;
    [SerializeField] private Tile tile2;
    [SerializeField] private Tile tile3;
    [SerializeField] private Tile tile4;
    
    private Animator anim;
    
    private void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    void Update()
    {
        if(tile1.tilePushed)
        {
            if(tile2.tilePushed)
            {
                if(tile3.tilePushing && tile4.tilePushing)
                {
                    DoorOpen();
                }
            }
            else if(tile3.tilePushed || tile4.tilePushed)
            {
                tile1.tilePushed = false;
                tile2.tilePushed = false;
                tile3.tilePushed = false;
                tile4.tilePushed = false;
            }
        }
        else 
        {
            tile2.tilePushed = false;
        }
    }

    public void DoorOpen()
    {
        anim.SetBool("IsOpen", true);
    }
}
