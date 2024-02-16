using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Base : MonoBehaviour
{
    public enum UIEvent
    {
        Click,
        Drag,
    }

    // Type을 Key로 사용하여, 내부 요소를 이름으로 갖는 오브젝트들을 관리.
    // ex) Key:TextMeshProUGUIs(type), Value[]:{PointText(Object), ScoreText(Object)}
    // enum TextMeshProUGUIs
    // {
    //     PointText,
    //     ScoreText,
    // }  
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    // 해당 enum 타입 중 T 컴포넌트를 갖는 이름이 동일한 프리팹 모두 바인드
    // ex) Bind<Button>(typeof(Buttons_enum));
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // 리플렉션 사용
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = FindChild(gameObject, names[i], true, true);
            else
                objects[i] = FindChild<T>(gameObject, names[i], true, true);

            if (objects[i] == null) // 디버그용
                Debug.Log($"Failed to bind({names[i]})");
        }
    }
    // 접근
    // ex) GameObject go = Get<Image>((int)Images.ItemIcon).gameObject
    // ex+) GameObject go = GetImage((int)Images.ItemIcon).gameObject
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;
        return objects[idx] as T;
    }
    protected TextMeshProUGUI GetTextMeshProUGUI(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }

    public void ClearBindings()
    {
        _objects.Clear();
    }

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        UI_EventHandler evt = Managers.UI.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }



    // 유틸성 메서드
    public GameObject FindChild(GameObject go, string name = null, bool recursive = false, bool includeInactive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive, includeInactive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    public T FindChild<T>(GameObject go, string name = null, bool recursive = false, bool includeInactive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        //Debug.Log($"Bind Transform: {name}"); // 디버그용
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>(includeInactive))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    //Debug.Log($"Bind Component: {name}"); // 디버그용
                    return component;
                }
            }
        }
        return null;
    }
}
