using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Clear : UI_Popup
{
    enum Buttons
    {
        BackTitleBtn,
        NextStageBtn,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.BackTitleBtn).onClick.AddListener(OnClickBackTitle); // 타이틀로 버튼 이벤트
        GetButton((int)Buttons.NextStageBtn).onClick.AddListener(OnClickNextStage); // 다음스테이지 버튼 이벤트
    }
    void OnClickBackTitle()
    {
        SceneManager.LoadScene(0);
    }
    void OnClickNextStage()
    {
        SceneManager.LoadScene(0); // 임시 작성.
    }
}
