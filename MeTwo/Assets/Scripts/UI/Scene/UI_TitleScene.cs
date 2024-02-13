using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    enum Buttons
    {
        GameStart,
        GameSettings,
        GameExit,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        // 게임 시작 버튼 이벤트
        GetButton((int)Buttons.GameStart).onClick.AddListener(OnClickGameStart);
        // 게임 설정 버튼 이벤트
        GetButton((int)Buttons.GameSettings).onClick.AddListener(OnClickGameSettings);
        // 게임 종료 버튼 이벤트
        GetButton((int)Buttons.GameExit).onClick.AddListener(OnClickGameExit);
    }

    void OnClickGameStart()
    {
        // 스테이지 선택 팝업 표시
        TempManagers.UI.ShowPopupUI<UI_StageSelect>();
    }

    void OnClickGameSettings()
    {
        // 설정 팝업 표시
        TempManagers.UI.ShowPopupUI<UI_Settings>();
    }

    void OnClickGameExit()
    {
        // 게임 종료 로직 처리
        Application.Quit();
    }
}