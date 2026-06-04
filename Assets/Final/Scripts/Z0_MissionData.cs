using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로젝트 창에서 마우스 우클릭 -> Create -> ScriptableObjects -> MissionData로 미션 파일 생성 가능
[CreateAssetMenu(fileName = "NewMission", menuName = "ScriptableObjects/MissionData")]
public class MissionData : ScriptableObject
{
    public int missionNumber;          // 미션 번호 (1~10)
    public string missionTitle;        // 미션 제목
    [TextArea(3, 10)]
    public string missionDescription;  // 미션 상세 설명 (줄바꿈 가능)
    public string targetSceneName;     // 이동할 씬 이름
}
