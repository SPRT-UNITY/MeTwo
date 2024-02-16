using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Display : UI_Base
{
    protected string[] messages;

    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    public void Initialize(string[] _messages)
    {
        messages = _messages;
    }
}