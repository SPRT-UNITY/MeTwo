using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 호출 예시 (messages[]의 매개변수, )
//Managers.UI.ShowPopupUI<UI_Alert2Btn>(messages: new string[] { "저장된 모든 정보를 삭제합니다" }, actions: new System.Action[] { () =>
//        {
//            PlayerPrefs.DeleteAll();
//            Managers.UI.ShowPopupUI<UI_Alert1Btn>(messages: new string[] { "데이터를 삭제하였습니다." });
//        } });


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

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        TextMeshProUGUI alertText = GetTextMeshProUGUI((int)TextMeshProUGUIs.AlertText);
        Button okButton = GetButton((int)Buttons.OkBtn);

        if(messages.Length>0) alertText.text = messages[0];
        okButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
        okButton.onClick.AddListener(OnClickOk); // Ok 버튼 클릭 시 팝업 닫기
        if (actions.Length > 0) okButton.onClick.AddListener(() => actions[0]()); // 새 액션 추가

    }

    void OnClickClose()
    {
        Managers.UI.ClosePopupUI();
    }
    void OnClickCancel()
    {
        Managers.UI.ClosePopupUI();
    }
    void OnClickOk()
    {
        Managers.UI.ClosePopupUI();
    }
}
