using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        ClearBindings();
    }
    public void SetAlert(string message)
    {
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);

        alertText.text = message;

        ClearBindings();
    }
}