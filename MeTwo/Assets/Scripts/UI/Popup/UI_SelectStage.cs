using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Content;

public class UI_SelectStage : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
        EnterBtn,
        Stage0Panel,
        Stage1Panel,
        Stage2Panel,
    }
    enum Images
    {
        BlockStage0,
        BlockStage1,
        BlockStage2,
        BlockStageN,
        CheckMark0,
        CheckMark1,
        CheckMark2,
        CheckMarkN,
    }
    Image[] CheckMarks;
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트
        GetButton((int)Buttons.EnterBtn).onClick.AddListener(OnClickEnter); // 입장 버튼 이벤트
        GetButton((int)Buttons.Stage0Panel).onClick.AddListener(() => OnClickStage(0)); // 튜토리얼 스테이지 클릭
        GetButton((int)Buttons.Stage1Panel).onClick.AddListener(() => OnClickStage(1)); // 스테이지1 클릭
        GetButton((int)Buttons.Stage2Panel).onClick.AddListener(() => OnClickStage(2)); // 스테이지2 클릭
        // ... 스테이지N 클릭

        // 현재 해금 스테이지의 가림막 비활성화
        Image[] Blocks = new Image[] {
            GetImage((int)Images.BlockStage0),
            GetImage((int)Images.BlockStage1),
            GetImage((int)Images.BlockStage2),
            GetImage((int)Images.BlockStageN),
        };
        int canEnter = TempManagers.LV.canEnter;
        int limEnter = TempManagers.LV.limEnter;
        for (int i=0; i<= (canEnter<limEnter?canEnter:limEnter); i++)
        {
            Blocks[i].gameObject.SetActive(false);
        }

        // 체크마크 목록
        CheckMarks = new Image[] {
            GetImage((int)Images.CheckMark0),
            GetImage((int)Images.CheckMark1),
            GetImage((int)Images.CheckMark2),
            GetImage((int)Images.CheckMarkN),
        };

        // 현재 선택된 스테이지 초기화
        TempManagers.LV.nowEnter = -1;

    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnClickEnter()
    {
        if(TempManagers.LV.nowEnter != -1)
            SceneManager.LoadScene(1);
    }
    void OnClickStage(int num)
    {
        TempManagers.LV.nowEnter = num;
        CheckSelectStage(num);
    }
    void CheckSelectStage(int num)
    {
        for(int i=0;i< CheckMarks.Length; i++)
        {
            if (i == num) CheckMarks[i].gameObject.SetActive(true);
            else CheckMarks[i].gameObject.SetActive(false);
        }
    }
}