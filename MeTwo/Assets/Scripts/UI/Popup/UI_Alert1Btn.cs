using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        ClearBindings();
    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickOk()
    {
        TempManagers.UI.ClosePopupUI();
    }
    public void SetAlert(string message)
    {
        Debug.Log("1버튼 열렸다 사라짐");
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);

        alertText.text = message;

        ClearBindings();
    }
}
