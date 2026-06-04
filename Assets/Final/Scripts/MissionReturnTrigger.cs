using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionReturnTrigger : MonoBehaviour
{
    public int currentMissionNumber; // 현재 이 씬이 몇 번 미션인지 인스펙터에서 지정 (1~10)
    public string hubSceneName = "MainHubScene"; // 메인 연구실 씬 이름

    public void ReturnWithSuccess()
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.ClearMission(currentMissionNumber);
        }

        //  VRSceneLoader 대신 프로젝트에 맞춰 SceneLoader로 변경!
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(hubSceneName);
        }
        else
        {
            SceneManager.LoadScene(hubSceneName);
        }
    }

    public void ReturnWithFailure(string targetSceneName)
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.FailMission(currentMissionNumber);
            Debug.Log($"{currentMissionNumber}번 미션 실패 장부 기록 완료!");
        }

        //  여기도 SceneLoader로 변경!
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(targetSceneName);
        }
        else
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}