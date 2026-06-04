using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; 

public class Zone6SimulationError : MonoBehaviour
{
    [Header("[UI 연결] 경고창 부모 오브젝트 (처음엔 꺼둘 것)")]
    public GameObject errorPanel;

    [Header("[UI 연결] 깜빡거릴 TMP 경고 텍스트 (빨간색 권장)")]
    public TextMeshProUGUI warningText; 

    [Header("[설정] 씬 시작 후 몇 초 뒤에 경고창을 띄울까요?")]
    public float delayTime = 3.0f;

    private string hubSceneName = "MainHubScene"; // 복귀할 메인 연구실 이름

    void Start()
    {
        if (errorPanel != null) errorPanel.SetActive(false);

        // 지정된 시간 뒤에 경고창이 팝업되도록 예약 코루틴 실행
        StartCoroutine(ShowErrorRoutine());
    }

    IEnumerator ShowErrorRoutine()
    {
        yield return new WaitForSeconds(delayTime);

        // 1. 에러 패널 쾅! 켜기
        if (errorPanel != null)
        {
            errorPanel.SetActive(true);
            Debug.Log(" 위-험! 시뮬레이션 과부하 경고창 활성화!");
        }

        // 2. "위험!" 텍스트가 사이렌처럼 빨갛게 깜빡거리는 연출
        if (warningText != null)
        {
            warningText.text = "위험!";
            StartCoroutine(BlinkTextRoutine());
        }
    }

    IEnumerator BlinkTextRoutine()
    {
        while (true)
        {
            warningText.enabled = !warningText.enabled; // 텍스트 켰다 껐다 반복
            yield return new WaitForSeconds(0.4f); // 0.4초 간격
        }
    }

    // [핵심 버튼 함수] HUD의 [초기화] 버튼 온클릭에 이 함수를 연결하세요!
    public void OnClickResetButton()
    {
        Debug.Log(" 플레이어가 초기화 버튼을 눌렀습니다! 시스템 리셋 진입.");

        // 우리가 만들어둔 싱글톤 SceneLoader 재활용!
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(hubSceneName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(hubSceneName);
        }
    }
}