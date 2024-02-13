using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SoundManagerTest : MonoBehaviour
{
    [SerializeField]
    AssetLabelReference assetLabel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("playSFX", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void playSFX() 
    {

        SoundManager.Instance.PlaySFX("Player_Footstep_01");
    }
}
