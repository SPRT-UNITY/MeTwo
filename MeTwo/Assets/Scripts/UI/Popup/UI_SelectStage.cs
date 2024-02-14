using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_SelectStage : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
        EnterBtn,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트
        GetButton((int)Buttons.EnterBtn).onClick.AddListener(OnClickEnter); // 입장 버튼 이벤트
    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickEnter()
    {
        SceneManager.LoadScene(1);
    }
}