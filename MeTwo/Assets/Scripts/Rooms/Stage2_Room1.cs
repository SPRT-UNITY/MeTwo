using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Room1 : MonoBehaviour
{
    [SerializeField] private Material newMat;
    [SerializeField] private GameObject door;
    public GameObject[] objects1 = new GameObject[9];
    public GameObject[] objects2 = new GameObject[9];
    private Tile[] tiles1 = new Tile[9];
    private Tile[] tiles2 = new Tile[9];
    
    private Animator anim;
    private int i = 0;
    
    private void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            tiles1[i] = objects1[i].GetComponent<Tile>();
            tiles2[i] = objects2[i].GetComponent<Tile>();
        }
        anim = door.GetComponent<Animator>();
    }

    void Update()
    {
        if(i < 9)
        {
            if(tiles1[i].tilePushing && tiles2[i].tilePushing)
            {
                tiles1[i].tilePushed = true;
                tiles2[i].tilePushed = true;
                objects1[i].GetComponent<Renderer>().material = newMat;
                objects2[i].GetComponent<Renderer>().material = newMat;
                i++;
            }
        }
        else DoorOpen();
    }

    public void DoorOpen()
    {
        anim.SetBool("IsOpen", true);
    }
}
