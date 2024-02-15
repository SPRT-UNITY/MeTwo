using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interact : UI_Display
{
    // TempManagers.UI.ShowDisplayUI<UI_Interact>(messages: new string[] { "키", "내용" }); 으로 열기
    // TempManagers.UI.CloseDisplayUI(string DisplayName) 으로 닫기
    enum Texts
    {
        KeyText,
        DescriptionText,
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));

        // messages[0] 은 Key
        // messages[1] 은 내용
        if (messages.Length < 1) GetTextMeshProUGUI((int)Texts.KeyText).text = "F";
        else GetTextMeshProUGUI((int)Texts.KeyText).text = messages[0];
        if (messages.Length < 2) GetTextMeshProUGUI((int)Texts.DescriptionText).text = "상호작용하기";
        else GetTextMeshProUGUI((int)Texts.DescriptionText).text = messages[1];
    }
}
