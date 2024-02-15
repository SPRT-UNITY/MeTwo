using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region 씬 전환 직후 로드할 내용을 작성
    // Show 이외 다른 내용 넣어도 좋을 듯
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // sceneLoaded 이벤트에 메서드를 등록
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 오브젝트가 파괴될 때 이벤트에서 메서드를 제거
        // 로직 상 파괴될 일은 없지만, 정석대로 작성
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Popup Order 초기화
        TempManagers.UI.StackClear();

        if (scene.buildIndex == 1) // Game 씬
        {
            TempManagers.UI.ShowSceneUI<UI_Main>();
            GameSceneManager.Instance.InitGame();
            //TempManagers.SetStatePlaying();
        }
        if (scene.buildIndex == 0) // Title 씬
        {
            TempManagers.LV.nowEnter = -1;
            TempManagers.UI.ShowSceneUI<UI_TitleMenu>();
            //TempManagers.SetStateTitle();
        }
    }
    #endregion
}
