using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Z6_HUDGlitch : MonoBehaviour
{
    [Header("[색상 설정] 지직거릴 3가지 색상")]
    public Color color1 = new Color(0f, 1f, 0.75f);      // 민트
    public Color color2 = new Color(0.05f, 0.05f, 0.05f); // 검정
    public Color color3 = new Color(1f, 0f, 0.5f);       // 핫핑크

    [Header("[속도 설정] 깜빡이는 최소/최대 속도")]
    public float minSpeed = 0.03f;
    public float maxSpeed = 0.08f;

    private string glitchChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-[]{}|;':\",./<>?";

    void Start()
    {
        // 1. 하위의 모든 SpriteRenderer를 찾아서 '각각' 독립된 코루틴을 돌립니다.
        List<SpriteRenderer> allSprites = new List<SpriteRenderer>();
        GetComponentsInChildren<SpriteRenderer>(true, allSprites);

        foreach (var sprite in allSprites)
        {
            if (sprite != null)
            {
                StartCoroutine(PlaySpriteGlitch(sprite));
            }
        }

        // 2. 하위의 모든 TextMeshProUGUI를 찾아서 '각각' 독립된 코루틴을 돌립니다.
        List<TextMeshProUGUI> allTexts = new List<TextMeshProUGUI>();
        GetComponentsInChildren<TextMeshProUGUI>(true, allTexts);

        foreach (var textComp in allTexts)
        {
            if (textComp != null && !string.IsNullOrEmpty(textComp.text))
            {
                StartCoroutine(PlayTextGlitch(textComp, textComp.text));
            }
        }
    }

    //  [독립 루틴 1] 이미지 테두리들 전용: 각각 다른 타이밍과 색상으로 무한 반복
    IEnumerator PlaySpriteGlitch(SpriteRenderer sprite)
    {
        while (sprite != null)
        {
            // 이 스프라이트 혼자서만 쓸 색상 결정
            sprite.color = GetRandomGlitchColor();

            // 이 스프라이트 혼자서만 쉴 시간 결정 (따로따로 엇박자 나게 만들기)
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
        }
    }

    //  [독립 루틴 2] 텍스트들 전용: 각각 다른 타이밍, 색상, 외계어로 무한 반복
    IEnumerator PlayTextGlitch(TextMeshProUGUI textComp, string originalText)
    {
        while (textComp != null)
        {
            // 이 텍스트 혼자서만 쓸 색상 결정
            textComp.color = GetRandomGlitchColor();

            // 이 텍스트 혼자서만 바뀔 외계어 조합 생성
            char[] brokenText = new char[originalText.Length];
            for (int i = 0; i < originalText.Length; i++)
            {
                if (originalText[i] == ' ' || originalText[i] == '\n')
                {
                    brokenText[i] = originalText[i];
                }
                else
                {
                    brokenText[i] = glitchChars[Random.Range(0, glitchChars.Length)];
                }
            }
            textComp.text = new string(brokenText);

            // 이 텍스트 혼자서만 쉴 시간 결정
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
        }
    }

    //  3가지 색상 중 하나를 무작위로 반환하는 헬퍼 함수
    private Color GetRandomGlitchColor()
    {
        float randomChoice = Random.value;
        if (randomChoice < 0.33f) return color1;
        if (randomChoice < 0.66f) return color2;
        return color3;
    }
}