using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    private Animator btnAnim;
    public bool btnPushed;
    public bool timeLimit;
    public float timer;
    private float time;
    public int count = 0;

    private void Start()
    {
        time = timer;
        btnAnim = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if(btnPushed)
        {
            btnAnim.SetBool("IsPush", true);
        }
        else
        {
            btnAnim.SetBool("IsPush", false);
        }
        if(timeLimit && btnPushed)
        {
            timer -= Time.deltaTime;
            if(timer < 0) 
            {
                count++;
                btnPushed = false;
                timer = time;
                btnAnim.SetBool("IsPush", false);
            }
            else btnPushed = true;
        }
    }

    public void PushButton()
    {
        btnPushed = !btnPushed;
    }
}
