using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CloneIcon : UI_Scene
{
    enum Images
    {
        Icon_main_normal,
        Icon_main_chain,
        Icon_clone_normal,
        Icon_clone_chain,
    }
    Image[] CloneStatusIcons;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        CloneStatusIcons = new Image[] {
            GetImage((int)Images.Icon_main_normal),
            GetImage((int)Images.Icon_main_chain),
            GetImage((int)Images.Icon_clone_normal),
            GetImage((int)Images.Icon_clone_chain),
        };
        UpdateCloneStatusIcon();

        // 클론상태 바뀌는 이벤트에 구독
        // ??? += UpdateCloneStatusIcon();
        // 만약 구독하는 스크립트가 DontDestroyObject일 경우, 씬 전환 시에 해제 구독목록 초기화 할 수 있도록 SceneLoader에 장치 해주기

    }

    void UpdateCloneStatusIcon()
    {
        int cloneStatus = 0; // 임시작성 int cloneStatus = TempManagers.LV.cloneStatus;
        for (int i = 0; i < CloneStatusIcons.Length; i++)
        {
            if (i == cloneStatus) CloneStatusIcons[i].gameObject.SetActive(true);
            else CloneStatusIcons[i].gameObject.SetActive(false);
        }
    }
}
