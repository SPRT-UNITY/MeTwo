using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UIManager : MonoBehaviour
{

    int _order = 10; // Popup UI들에 부여할 order
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // Popup UI를 관리할 스택
    //UI_Scene _sceneUI = null; // Popup처럼 Scene 관리할 무언가도 필요하다면 마련

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

    // 카테고리:SceneUI 생성
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

        T sceneUI = GetOrAddComponent<T>(go);

        return sceneUI;
    }

    // 카테고리:PopupUI 생성
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
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

        T popup = GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        return popup;
    }

    // 카테고리:PopupUI 닫기
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        UI_Popup popup = _popupStack.Pop();
        Destroy(popup.gameObject);
        popup = null;
        _order--;
    }
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

}
