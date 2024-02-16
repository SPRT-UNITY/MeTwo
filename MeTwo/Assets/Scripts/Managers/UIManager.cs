using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class UIManager : MonoBehaviour
{

    int _order = 10; // Popup UI들에 부여할 order
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // Popup UI를 관리할 스택
    //UI_Scene _sceneUI = null; // Popup처럼 Scene 관리할 무언가도 필요하다면 마련
    List<UI_Display> _displayList = new List<UI_Display>(); // 

    public GameObject Root // UI들을 넣을 부모 오브젝트
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public int GetPopStackCount()
    {
        return _popupStack.Count;
    }

    // 외부에서 Popup같은 UI의 생성과 동시에, UIManager에게 SetCanvas를 요청하여 order 부여받음
    public void SetCanvas(GameObject go, bool sort = true)
    {
        
        Canvas canvas = GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    // 컴포넌트를 얻거나 추가하기, UI 외 요소에서도 사용 가능
    public T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component 
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    // SceneUI 생성
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (go == null)
        {
            Debug.LogError("프리팹 로드 실패");
            return null;
        }

        GameObject instantiatedGo = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity, Root.transform);
        instantiatedGo.name = instantiatedGo.name.Replace("(Clone)", "").Trim();

        T sceneUI = GetOrAddComponent<T>(instantiatedGo);

        return sceneUI;
    }

    // DisplayUI 생성
    public T ShowDisplayUI<T>(string name = null, string[] messages = null) where T : UI_Display
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (go == null)
        {
            Debug.LogError("프리팹 로드 실패");
            return null;
        }

        GameObject instantiatedGo = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity, Root.transform);
        instantiatedGo.name = instantiatedGo.name.Replace("(Clone)", "").Trim();

        T display = GetOrAddComponent<T>(instantiatedGo);
        display.Initialize(messages);
        _displayList.Add(display);

        return display;
    }
    // DisplayUI 삭제
    public void CloseDisplayUI(string name)
    {
        // 리스트에서 해당 이름을 가진 DisplayUI 찾기
        UI_Display displayToRemove = _displayList.Find(display => display.gameObject.name == name);
        if (displayToRemove != null)
        {
            // 리스트에서 제거
            _displayList.Remove(displayToRemove);

            // 게임 오브젝트 파괴
            Destroy(displayToRemove.gameObject);
        }
        else
        {
            Debug.LogError($"Failed to find DisplayUI name: {name}");
        }
    }

    // PopupUI 생성
    public T ShowPopupUI<T>(string name = null, string[] messages=null, Action[] actions = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (go == null)
        {
            Debug.LogError("프리팹 로드 실패");
            return null;
        }

        GameObject instantiatedGo = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity, Root.transform);
        instantiatedGo.name = instantiatedGo.name.Replace("(Clone)", "").Trim();

        T popup = GetOrAddComponent<T>(instantiatedGo);
        popup.Initialize(messages, actions);
        _popupStack.Push(popup);

        SoundManager.Instance.PlaySFX("UISelect");

        return popup;
    }

    // PopupUI 닫기
    public void ClosePopupUI(UI_Popup popup) // 이건... 사용하지 않도록. 아래의 매개변수 없이 하는 것을 권장.
    {
        if (_popupStack.Count == 0)
        {
            Debug.Log("_popupStack.Count == 0");
            return;
        }
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        SoundManager.Instance.PlaySFX("UIClose");

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
        {
            Debug.Log("_popupStack.Count == 0");
            return;
        }
        UI_Popup popup = _popupStack.Pop();
        Destroy(popup.gameObject);
        popup = null;
        _order--;


        SoundManager.Instance.PlaySFX("UIClose");
    }
    public void CloseAllPopupUI()
    {
        SoundManager.Instance.PlaySFX("UIClose");

        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
    public void StackClear() // 씬 전환시 스택 초기화
    {
        _popupStack.Clear();
    }

}
