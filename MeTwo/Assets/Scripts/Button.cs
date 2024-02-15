using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Animator btnAnim;
    public bool btnPushed;
    public bool timeLimit;
    public float timer;
    private float time;

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
                btnPushed = false;
                timer = time;
                btnAnim.SetBool("IsPush", false);
            }
        }
    }

    public void PushButton()
    {
        btnPushed = !btnPushed;
    }
}
