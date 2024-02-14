using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Alert2Btn : UI_Popup
{
    enum TextMeshProUGUIs
    {
        AlertText,
    }
    enum Buttons
    {
        CloseBtn,
        CancelBtn,
        OkBtn,
    }
    enum PreButtons
    {
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
        GetButton((int)Buttons.CancelBtn).onClick.AddListener(OnClickCancel); // 취소(=닫기) 버튼 이벤트
        ClearBindings();
    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickCancel()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickOk()
    {
        TempManagers.UI.ClosePopupUI();
    }
    public void SetAlert(string message, Action onOkPressed)
    {
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);
        Bind<Button>(typeof(PreButtons));
        Button okButton = GetButton((int)PreButtons.OkBtn);

        alertText.text = message;
        okButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
        okButton.onClick.AddListener(OnClickOk); // Ok 버튼 클릭 시 팝업 닫기
        okButton.onClick.AddListener(() => onOkPressed()); // 새 액션 추가

        ClearBindings();
    }
}
