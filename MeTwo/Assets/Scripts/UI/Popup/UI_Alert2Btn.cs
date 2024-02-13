using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Alert2Btn : UI_Popup
{
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
        GetButton((int)Buttons.OkBtn).onClick.AddListener(OnClickOk); // 확인 시 이벤트
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
        // UI Manager에서 현재 팝업창에서 실행될 메서드를 전달받아 실행하도록 수정
        // 임시 : 저장된 데이터 삭제

        PlayerPrefs.DeleteAll();
        // UI Manager에서 현재 팝업창에서 실행될 메서드 목록 삭제하는 구문 추가
        TempManagers.UI.ClosePopupUI();
        // 데이터 삭제의 경우, 데이터가 삭제되었다는 2초정도 Raycast 받지 않는 팝업 메시지 띄우기
        Debug.Log("데이터를 삭제하였습니다.");
    }
}
