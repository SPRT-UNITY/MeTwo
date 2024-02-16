using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_CallPause : MonoBehaviour
{
    void Update()
    {
        // Escape, P 키를 눌렀을 때 Pause를 열거나 팝업을 닫음
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !GameSceneManager.Instance.GetIsCleared())
        {
            if (TempManagers.UI.GetPopStackCount() > 0)
            {
                TempManagers.UI.ClosePopupUI();
                if (TempManagers.UI.GetPopStackCount() == 0)
                    TempManagers.SetStatePlaying();
            }
            else
            {
                TempManagers.UI.ShowPopupUI<UI_Pause>();
                TempManagers.SetStatePause();
            }
        }
    }
}