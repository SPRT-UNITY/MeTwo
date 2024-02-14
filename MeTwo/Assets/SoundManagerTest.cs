using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SoundManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("playBGM", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void playBGM() 
    {

        SoundManager.Instance.PlayBGM("Temp");
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX("Land");
    }
}
