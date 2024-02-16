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
            if (Managers.UI.GetPopStackCount() > 0)
            {
                Managers.UI.ClosePopupUI();
                if (Managers.UI.GetPopStackCount() == 0)
                    Managers.SetStatePlaying();
            }
            else
            {
                Managers.UI.ShowPopupUI<UI_Pause>();
                Managers.SetStatePause();
            }
        }
    }
}