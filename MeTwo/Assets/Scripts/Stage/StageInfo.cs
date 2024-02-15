using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStageData", menuName = "Stage", order = 0)]
public class StageInfo : ScriptableObject
{
    public int sectorNum;
    
    
    public GameObject stagePrefab;
}
