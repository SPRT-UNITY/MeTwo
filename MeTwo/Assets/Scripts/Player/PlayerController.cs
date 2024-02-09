using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<Vector2> onMoveEvent;
    public Action<Vector2> onLookEvent;
    public Action onInteractEvent;


    public void CallMoveEvent(Vector2 direction)
    {
        onMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        onLookEvent?.Invoke(direction);
    }

    public void CallInteractionEvent()
    {
        onInteractEvent?.Invoke();
    }
}
