using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 호출 예시
// TempManagers.UI.ShowPopupUI<UI_Alert1Btn>(messages: new string[] { "데이터를 삭제하였습니다." });

public class UI_Alert1Btn : UI_Popup
{
    enum TextMeshProUGUIs
    {
        AlertText,
    }
    enum Buttons
    {
        CloseBtn,
        OkBtn,
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
        GetButton((int)Buttons.OkBtn).onClick.AddListener(OnClickOk); // 확인 시 이벤트

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);

        if (messages.Length > 0) alertText.text = messages[0];

    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickOk()
    {
        TempManagers.UI.ClosePopupUI();
    }
}
