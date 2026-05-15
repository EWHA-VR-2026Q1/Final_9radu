using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Z0_FadeIn : MonoBehaviour
{
    public CanvasGroup blackPanelGroup; // 인스펙터에서 BlackPanel 드래그앤드롭
    public float fadeDuration = 2.0f;    // 2초 동안 밝아짐

    // 버튼에 연결할 함수
    public void StartSimulation()
    {
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // Alpha 값을 1에서 0으로 서서히 줄임
            blackPanelGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        // 완전히 투명해지면 오브젝트를 꺼서 클릭 방해 안 되게 함
        blackPanelGroup.gameObject.SetActive(false);
    }
}
