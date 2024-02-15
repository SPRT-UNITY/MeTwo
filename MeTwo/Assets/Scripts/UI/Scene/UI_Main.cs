using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum Buttons
    {
        PauseBtn,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.PauseBtn).onClick.AddListener(OnClickPause); // 일시정지 이벤트

    }

    void OnClickPause()
    {
        TempManagers.UI.ShowPopupUI<UI_Pause>();
        //TempManagers.SetStatePause();
    }
}
