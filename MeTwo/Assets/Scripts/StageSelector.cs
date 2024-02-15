using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    private static StageSelector instance;
    public static StageSelector Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = GameObject.Find("StageSelector");

                if (gameObject == null)
                {
                    gameObject = new GameObject("StageSelector");
                    instance = gameObject.AddComponent<StageSelector>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    Stage[] StagePrefabs;

    int currentStage = -1;

    // Stage의 클리어 여부는 ClearTime이 0보다 큰지 아닌지로 판단
    // 입장 가능 스테이지는 목록 중에 ClearTime이 0인 가장 첫 인덱스
    // 이후 스테이지들은 전부 입장 불가

    public int numOfStages { get { return StagePrefabs.Length; } }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        StagePrefabs = Resources.LoadAll<Stage>("Prefabs/Stages/");
        StagePrefabs = StagePrefabs.OrderBy(stage => stage.name).ToArray();

        // 임시로 바로 0으로 지정하게 하였음
        //currentStage = 0;
        foreach (var stage in StagePrefabs)
        {
            Debug.Log(stage.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectStage(int stage) 
    {
        currentStage = stage;
    }

    public GameObject loadStage() 
    {
        // 아래 한 줄 임시조치
        SelectStage(TempManagers.LV.nowEnter);
        return Instantiate(StagePrefabs[currentStage].gameObject);
    }

    public void DeleteStageDatas() 
    {
        foreach (var item in StagePrefabs) 
        {
            item.clearTime = 0.0f;
        }
    }
}
