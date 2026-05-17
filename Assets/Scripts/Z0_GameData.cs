using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionState
{
    NotStarted, // 아직 안함 (기본값)
    Success,    // 미션 성공
    Failed      // 미션 실패
}

public class Z0_GameData : MonoBehaviour
{
    // 미션 1~10번의 상태를 담는 배열 (기본값은 전부 NotStarted)
    public static MissionState[] missionStates = new MissionState[11];

    public static int currentMissionNumber = 1;
}
