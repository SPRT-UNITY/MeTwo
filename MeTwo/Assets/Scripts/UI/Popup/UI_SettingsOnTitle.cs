using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SettingsOnTitle : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
        ResetDataBtn,
    }
    // 사운드 및 감도 조절 관련 상태 enum 추가 예정

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트
        GetButton((int)Buttons.ResetDataBtn).onClick.AddListener(OnClickResetData); // 데이터 삭제 버튼 이벤트
    }
    public void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    public void OnClickResetData()
    {
        TempManagers.UI.ShowPopupUI<UI_Alert2Btn>();
    }
}