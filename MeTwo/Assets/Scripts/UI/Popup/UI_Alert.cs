using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 호출 예시
// Managers.UI.ShowPopupUI<UI_Alert>(messages: new string[] { "데이터를 삭제하였습니다." });

public class UI_Alert : UI_Popup
{
    enum TextMeshProUGUIs
    {
        AlertText,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);

        if (messages.Length > 0) alertText.text = messages[0];

        ClearBindings();
    }
}