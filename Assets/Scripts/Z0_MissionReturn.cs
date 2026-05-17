using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Z0_MissionReturn : MonoBehaviour
{
    [Header("현재 미션 번호 (1~10)")]
    public int missionNumber;

    [Header("이 버튼을 누를 때의 미션 결과")]
    public MissionState resultState; // 인스펙터에서 Success 또는 Failed 선택 가능!

    // 복귀 버튼 레이저 클릭 시 호출
    public void ReturnToHub()
    {
        if (missionNumber >= 1 && missionNumber <= 10)
        {
            // 인스펙터에서 설정한 결과(성공 또는 실패)를 저장소에 기록!
            Z0_GameData.missionStates[missionNumber] = resultState;
        }

        // 연구실 메인 허브 씬으로 복귀
        SceneManager.LoadScene("ZO_S0_Start");
    }
}
