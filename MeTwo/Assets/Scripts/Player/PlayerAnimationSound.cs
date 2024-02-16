using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSound : MonoBehaviour
{
    public void PlayWalkSound() 
    {
        SoundManager.Instance.PlaySFX("Walk");
    }
    public void PlayLandSound() 
    {
        SoundManager.Instance.PlaySFX("Land");
    }
    public void PlayJumpSound() 
    {
        SoundManager.Instance.PlaySFX("Jump");
    }
}
