using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UI_TitleMenu : UI_Scene
{
    enum Buttons
    {
        SelectStageBtn,
        SettingsBtn,
        GameExitBtn,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.SelectStageBtn).onClick.AddListener(OnClickGameStart); // 게임 시작 버튼 이벤트
        GetButton((int)Buttons.SettingsBtn).onClick.AddListener(OnClickGameSettings); // 게임 설정 버튼 이벤트
        GetButton((int)Buttons.GameExitBtn).onClick.AddListener(OnClickGameExit); // 게임 종료 버튼 이벤트
    }

    // 스테이지 선택 팝업 표시
    void OnClickGameStart()
    {
        Managers.UI.ShowPopupUI<UI_SelectStage>();
    }

    // 설정 팝업 표시
    void OnClickGameSettings()
    {
        Managers.UI.ShowPopupUI<UI_SettingsOnTitle>();
    }

    // 게임 종료
    void OnClickGameExit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}