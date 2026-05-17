using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionUIManager : MonoBehaviour
{
    [Header("오른쪽 상세 정보 UI 연결")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    // 현재 플레이어가 선택해서 대기 중인 미션 데이터를 저장할 변수
    private MissionData selectedMission;

    // 왼쪽 버튼들이 클릭되었을 때 이 함수 호출
    public void UpdateMissionDetails(MissionData data)
    {
        if (data == null) return;

        selectedMission = data;

        // 오른쪽 텍스트들 교체
        titleText.text = data.missionTitle;
        descriptionText.text = data.missionDescription;
    }

    [Header("10개 미션 버튼의 클리어 표시 오브젝트들")]
    // 인스펙터에서 1번~10번 버튼 옆에 달아둔 '체크 이미지'나 '성공 불빛'을 순서대로 넣을 배열
    public GameObject[] missionCheckMarks = new GameObject[11];

    [Header("10개 미션 버튼의 실패 표시들 (X표시)")]
    public GameObject[] missionFailMarks = new GameObject[11];

    void Start()
    {
        // 연구실 씬이 켜지자마자 살아남은 데이터 매니저(장부)를 검사
        UpdateCheckMarksFromData();
    }

    // 장부를 읽어서 UI를 갱신하는 함수
    public void UpdateCheckMarksFromData()
    {
        if (GameDataManager.Instance == null) return;

        // 1번부터 10번 미션까지 장부를 싹 훑어본다
        for (int i = 1; i <= 10; i++)
        {
            if (missionCheckMarks[i] != null)
            {
                // 장부에 true(클리어)라고 적혀있으면 체크마크 오브젝트를 활성화(true), 아니면 비활성화
                missionCheckMarks[i].SetActive(GameDataManager.Instance.isMissionCleared[i]);
            }
            if (missionFailMarks[i] != null)
            {
                // fail로 적혀있으면 X 오브젝트 활성화
                missionFailMarks[i].SetActive(GameDataManager.Instance.isMissionFailed[i]);
            }
        }
    }

    // 오른쪽의 '입장(Accept) 버튼'을 누르면 실행될 함수
    public void OnClickAcceptButton()
    {
        if (selectedMission != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(selectedMission.targetSceneName);
        }
    }
}