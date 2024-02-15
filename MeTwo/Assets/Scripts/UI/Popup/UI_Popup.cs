using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    protected string[] messages;
    protected Action[] actions;

    public virtual void Init()
    {
        TempManagers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        TempManagers.UI.ClosePopupUI(this);
    }
    public void Initialize(string[] _messages, Action[] _actions)
    {
        messages = _messages;
        actions = _actions;
    }
}
