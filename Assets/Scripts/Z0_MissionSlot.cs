using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSlot : MonoBehaviour
{
    [Header("이 버튼이 담당할 미션 데이터 파일")]
    public MissionData myMissionData;

    private MissionUIManager uiManager;

    void Start()
    {
        // 최상위에 있는 매니저를 자동으로 찾아와서 연결
        uiManager = GetComponentInParent<MissionUIManager>();
    }

    // 레이저로 이 버튼을 클릭했을 때 실행할 함수
    public void OnSlotClick()
    {
        if (uiManager != null && myMissionData != null)
        {
            // 매니저한테 내 데이터를 넘겨주면서 "오른쪽 화면 갱신해줘!" 요청하기
            uiManager.UpdateMissionDetails(myMissionData);
        }
    }
}