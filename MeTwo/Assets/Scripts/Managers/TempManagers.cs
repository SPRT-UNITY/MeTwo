using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManagers : MonoBehaviour
{
    static TempManagers s_instance;
    static TempManagers Instance { get { Init(); return s_instance; } }

    UIManager _ui;
    public static UIManager UI { get { return Instance._ui; } }

    SceneLoader _sl;
    public static SceneLoader SL { get { return Instance._sl; } }

    // 게임이 로드될 때 자동으로 실행
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod() 
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            // @Managers 오브젝트를 찾거나 만들어 TempManagers 컴포넌트를 Add
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

            // SceneLoader 컴포넌트 추가 및 참조 설정
            if (go.GetComponent<SceneLoader>() == null)
            {
                s_instance._sl = go.AddComponent<SceneLoader>();
            }
            else
            {
                s_instance._sl = go.GetComponent<SceneLoader>();
            }

        }
    }
    // 상황에 따른 '마우스 가운데잠금 및 보이기', '타임스케일' 조정
    // 일단 적당한 Manager가 없어 여기에 넣음. SceneLoader와 UI_Main, UI_Pause에서 사용.
    // ESC로 일시정지 버튼을 누르는 기능이 없어 일단 주석처리 해놓음.
    static public void SetStateTitle()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
    static public void SetStatePause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    static public void SetStatePlaying()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
