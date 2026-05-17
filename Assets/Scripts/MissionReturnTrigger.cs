using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionReturnTrigger : MonoBehaviour
{
    public int currentMissionNumber; // 현재 이 씬이 몇 번 미션인지 인스펙터에서 지정 (1~10)
    public string hubSceneName = "MainHubScene"; // 메인 연구실 씬 이름

    // 1. 성공해서 연구실로 돌아갈 때 호출할 함수 (성공 버튼이나 탈출 구역에 연결)
    public void ReturnWithSuccess()
    {
        // 데이터 매니저한테 성공했다고 장부 기록 요청
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.ClearMission(currentMissionNumber);
        }

        // 메인 연구실로 씬 로드
        SceneManager.LoadScene(hubSceneName);
    }

    // 2. 실패해서 돌아가거나, 한계 씬에서 확장 씬으로 바로 넘어갈 때 호출할 함수
    public void ReturnWithFailure(string targetSceneName)
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.FailMission(currentMissionNumber);
            Debug.Log($"{currentMissionNumber}번 미션 실패 장부 기록 완료!");
        }

        // 2. 그 다음 기존 코드대로 S2 확장 씬으로 이동시킨다.
        UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
    }
}