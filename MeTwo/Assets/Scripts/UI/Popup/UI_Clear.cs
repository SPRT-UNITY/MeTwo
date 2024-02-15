using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    enum TextMeshProUGUIs
    {
        ScoreText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        GetButton((int)Buttons.BackTitleBtn).onClick.AddListener(OnClickBackTitle); // 타이틀로 버튼 이벤트
        GetButton((int)Buttons.NextStageBtn).onClick.AddListener(OnClickNextStage); // 다음스테이지 버튼 이벤트

        // 점수관련한 처리에 대한 호출은 어디서 할지 추후 생각.
        // 일단 여기서는 화면에 보여주기만 하면 될 듯.
        switch(TempManagers.LV.nowEnter)
        {
            case 0:
                Get<Text>((int)TextMeshProUGUIs.ScoreText).text = $"걸린 시간 : <b>{TempManagers.LV.scoreFloor0}</b>초";
                break;
            case 1:
                Get<Text>((int)TextMeshProUGUIs.ScoreText).text = $"걸린 시간 : <b>{TempManagers.LV.scoreFloor1}</b>초";
                break;
            case 2:
                Get<Text>((int)TextMeshProUGUIs.ScoreText).text = $"걸린 시간 : <b>{TempManagers.LV.scoreFloor2}</b>초";
                break;
        }
    }
    void OnClickBackTitle()
    {
        SceneManager.LoadScene(0);
    }
    void OnClickNextStage()
    {
        TempManagers.LV.nowEnter += 1;
        SceneManager.LoadScene(1);
    }
}
