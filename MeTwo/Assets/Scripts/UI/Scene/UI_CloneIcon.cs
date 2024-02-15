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
    Image[] ControlStatusIcons;
    int ControlStatusChain;
    int ControlStatusSwap;
    void Start()
    {
        Init();

        // PlayerManager 찾아서 구독하기..
        ControlStatusChain = 0;
        ControlStatusSwap = 0;

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.onBothCharacterControlEvent += ChainControlStatusIcon;
            playerManager.onSwapCharacterEvent += SwapControlStatusIcon;
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        ControlStatusIcons = new Image[] {
            GetImage((int)Images.Icon_main_normal),
            GetImage((int)Images.Icon_main_chain),
            GetImage((int)Images.Icon_clone_normal),
            GetImage((int)Images.Icon_clone_chain),
        };
        UpdateControlStatusIcon();
    }

    void SwapControlStatusIcon()
    {
        ControlStatusSwap = (ControlStatusSwap + 1) % 2;
        UpdateControlStatusIcon();
    }
    void ChainControlStatusIcon()
    {
        ControlStatusChain = (ControlStatusChain + 1) % 2;
        UpdateControlStatusIcon();
    }
    void UpdateControlStatusIcon()
    {
        int playerStatus = ControlStatusSwap*2 + ControlStatusChain;
        for (int i = 0; i < ControlStatusIcons.Length; i++)
        {
            if (i == playerStatus) ControlStatusIcons[i].gameObject.SetActive(true);
            else ControlStatusIcons[i].gameObject.SetActive(false);
        }
    }
}
