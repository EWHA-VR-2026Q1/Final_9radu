using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위한 필수 네임스페이스

public class SimpleSceneLoader : MonoBehaviour
{
    // 💡 인스펙터 창에서 이동하고 싶은 다음 씬 이름을 자유롭게 적을 수 있는 칸
    [Header("이동할 목표 씬 이름")]
    public string hubSceneName = "MainHubScene";

    // 💡 VR 버튼이나 레이캐스트 이벤트에서 호출할 함수
    public void LoadTargetScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(hubSceneName);
        }
        else
        {
            SceneManager.LoadScene(hubSceneName);
        }
    }
}