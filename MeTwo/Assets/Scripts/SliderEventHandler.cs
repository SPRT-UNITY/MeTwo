using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void SliderEvent();
    public event SliderEvent OnDragStart;
    public event SliderEvent OnDragEnd;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDragStart?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDragEnd?.Invoke();
    }
}
