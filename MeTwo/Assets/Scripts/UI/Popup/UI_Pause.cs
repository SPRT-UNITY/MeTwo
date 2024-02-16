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
        Managers.UI.ClosePopupUI();
    }
    void OnClickResetStage()
    {
        SoundManager.Instance.PlaySFX("UISelect");
        SceneManager.LoadScene(1);
    }
    void OnClickSettings()
    {
        Managers.UI.ShowPopupUI<UI_SettingsOnGame>();
    }
    void OnClickBackTitle()
    {
        SoundManager.Instance.PlaySFX("UISelect");
        SceneManager.LoadScene(0);
    }
    public void OnDestroy()
    {
        Managers.SetStatePlaying();
    }

}
