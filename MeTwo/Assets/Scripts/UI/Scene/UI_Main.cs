using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum Buttons
    {
        PauseBtn,
    }
    enum TextMeshProUGUIs
    {
        TimeText,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        timer = 0f;

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        GetButton((int)Buttons.PauseBtn).onClick.AddListener(OnClickPause); // 일시정지 이벤트
        timeText = GetTextMeshProUGUI((int)TextMeshProUGUIs.TimeText);

    }
    float timer;
    TextMeshProUGUI timeText;
    void Update()
    {
        timer += Time.deltaTime;
        timeText.text = $"{((int)timer/60).ToString("00")} : {((int)timer % 60).ToString("00")}";
    }

    void OnClickPause()
    {
        Managers.UI.ShowPopupUI<UI_Pause>();
        Managers.SetStatePause();
    }
}
