using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
        ResetStageBtn,
        SettingsBtn,
        BackTitleBtn,
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
        GetButton((int)Buttons.ResetStageBtn).onClick.AddListener(OnClickResetStage); // 처음부터 버튼 이벤트
        GetButton((int)Buttons.SettingsBtn).onClick.AddListener(OnClickSettings); // 환경설정 버튼 이벤트
        GetButton((int)Buttons.BackTitleBtn).onClick.AddListener(OnClickBackTitle); // 타이틀로 버튼 이벤트
    }

    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickResetStage()
    {
        SceneManager.LoadScene("UI_Base_Game");
    }
    void OnClickSettings()
    {
        TempManagers.UI.ShowPopupUI<UI_SettingsOnGame>();
    }
    void OnClickBackTitle()
    {
        SceneManager.LoadScene("UI_Base_Title");
    }
    public void OnDestroy() // 파괴 시 재개
    {
        //TempManagers.SetStatePlaying();
    }

}
