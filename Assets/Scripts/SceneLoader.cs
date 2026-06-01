using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    //  외부에서 SceneLoader.Instance로 접근할 수 있도록 싱글톤 정의!
    public static SceneLoader Instance;

    public Image fadeImage;       // 눈앞을 가릴 검은색 UI 이미지
    public float fadeDuration = 1.0f; // 페이드 아웃/인 시간 (1초)

    void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            // 씬이 바뀌어도 페이드 캔버스가 파괴되지 않고 유지되길 원한다면 아래 주석을 해제해줘!
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 맵에 입장할 때 자동으로 까만 화면에서 서서히 밝아짐 (Fade In)
        if (fadeImage != null)
        {
            StartCoroutine(Fade(1f, 0f));
        }
    }

    //  MissionReturnTrigger에서 불러다 쓸 페이드 로드 함수
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(TransitionRoutine(sceneName));
    }

    IEnumerator TransitionRoutine(string sceneName)
    {
        // 1. 화면을 서서히 까맣게 만듦 (Fade Out)
        yield return Fade(0f, 1f);

        // 2. 완전히 까맣게 변한 직후에 다음 씬으로 로드!
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}