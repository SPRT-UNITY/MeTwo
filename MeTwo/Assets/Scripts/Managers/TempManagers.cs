using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManagers : MonoBehaviour
{
    static TempManagers s_instance;
    static TempManagers Instance { get { Init(); return s_instance; } }

    UIManager _ui;
    public static UIManager UI { get { return Instance._ui; } }

    // 게임이 로드될 때 자동으로 실행
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod() 
    {
        Init();
    }

    // @Managers 오브젝트를 찾거나 만들어 Managers 컴포넌트를 Add
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                s_instance = go.AddComponent<TempManagers>();
                DontDestroyOnLoad(go);
            }
            else
            {
                s_instance = go.GetComponent<TempManagers>();
            }

            // UIManager 컴포넌트 추가 및 참조 설정
            if (go.GetComponent<UIManager>() == null)
            {
                s_instance._ui = go.AddComponent<UIManager>();
            }
            else
            {
                s_instance._ui = go.GetComponent<UIManager>();
            }

            s_instance._ui.ShowSceneUI<UI_TitleMenu>();
        }
    }
}
