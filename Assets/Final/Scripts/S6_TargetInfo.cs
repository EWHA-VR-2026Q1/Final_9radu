using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zone3TargetInfo : MonoBehaviour
{
    [Header("[선택] 이 오브젝트가 가스통인가요? (가스통이면 체크, 생존자면 해제)")]
    public bool isGasTank = true;

    [Header("[연결] 플레이어 눈앞의 HUD 텍스트")]
    public TextMeshProUGUI hudInfoText;

    [Header("[연결] 미션 타이머 (성공 UI 호출용)")]
    public MissionTimer missionTimer;

    [Header("[연결] 미션 완료 성공 UI 오브젝트")]
    public GameObject successUI;

    private bool isPlayerHovering = false; // 레이저가 현재 나를 조준 중인가?
    private bool isRescued = false;
    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    void Update()
    {
        //  가스통이 아니고 생존자일 때 + 레이저가 조준하고 있는 상태라면!
        if (!isGasTank && !isRescued && isPlayerHovering)
        {
            // 오큘러스 컨트롤러의 그랩(HandTrigger)이나 검지 트리거를 누르면 무조건 성공!
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ||
                OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) ||
                OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) ||
                OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                RescueSuccess();
            }
        }
    }

    //  [레이저 전용 함수] 왼손 레이저가 오브젝트를 조준했을 때 (Event_On_Ray 연동)
    public void OnRayEnterTarget()
    {
        isPlayerHovering = true; // 조준 상태 켜기
        if (outline != null) outline.enabled = true;

        if (isGasTank)
        {
            if (hudInfoText != null)
                hudInfoText.text = "<color=red>\n\n[위험] 고압 가스통 발견\n\n접근 금지!</color>";
        }
        else if (!isRescued)
        {
            if (hudInfoText != null)
                hudInfoText.text = "<color=green>\n\n[확인] 생존자 발견!</color>";
        }
    }

    //  [레이저 전용 함수] 왼손 레이저가 조준을 벗어났을 때
    public void OnRayExitTarget()
    {
        isPlayerHovering = false; // 조준 상태 끄기
        if (outline != null) outline.enabled = false;

        if (hudInfoText != null && !isRescued)
        {
            hudInfoText.text = "\n\nVisibility Assist: ON";
        }
    }

    void RescueSuccess()
    {
        isRescued = true;
        if (outline != null) outline.enabled = false;
        if (missionTimer != null) missionTimer.CompleteMission();

        if (successUI != null)
        {
            successUI.SetActive(true);
        }

        if (missionTimer != null)
            missionTimer.CompleteMission();
    }
}