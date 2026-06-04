using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // 어디서든 이 매니저에 접근할 수 있게 만드는 통로 (싱글톤)
    public static GameDataManager Instance { get; private set; }

    [Header("미션 클리어 여부 기록 (방패 모양 체크마크용)")]
    // 각 미션 번호(1~10)의 클리어 상태를 저장할 배열
    public bool[] isMissionCleared = new bool[11];

    private void Awake()
    {
        // 씬이 바뀌어도 이 오브젝트가 중복 생성되지 않고 오직 하나만 유지되도록 방어
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 핵심: 씬이 바뀌어도 메모리에서 지우지 마라!
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("미션 실패 여부 기록 (X표시)")]
    public bool[] isMissionFailed = new bool[11];

    // 미션을 성공했을 때 호출할 함수
    public void ClearMission(int missionNumber)
    {
        if (missionNumber >= 1 && missionNumber <= 10)
        {
            isMissionCleared[missionNumber] = true;
            Debug.Log($"{missionNumber}번 미션 성공 기록 완료!");
        }
    }
    
    // 미션을 실패했을 때 호출할 함수
    public void FailMission(int missionNumber)
    {
        if (missionNumber >= 1 && missionNumber <= 10)
        {
            isMissionFailed[missionNumber] = true; // 실패 장부에 true 체크
            Debug.Log($"{missionNumber}번 미션 실패 기록 완료...");
        }
    }
}