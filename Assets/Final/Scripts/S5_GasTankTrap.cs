using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTankTrap : MonoBehaviour
{
    [Header("[연결] 플레이어 오브젝트를 직접 드래그해서 넣어주세요!")]
    public Transform playerTransform;

    [Header("[설정] 폭발 감지 거리")]
    public float explosionRadius = 2.5f;

    [Header("[연결] 폭발 이펙트 (파티클 프리랩)")]
    public GameObject explosionEffect;

    [Header("[연결] 씬의 MissionTimer 오브젝트")]
    public MissionTimer missionTimer;

    private bool isExploded = false;

    void Start()
    {
        // 만약 인스펙터에서 깜빡하고 연결 안 했을 때만 자동으로 찾도록 예외 처리
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player") ?? GameObject.Find("Player_OVRInput_Combined_V2_S5");
            if (player != null) playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (isExploded || playerTransform == null) return;

        // 플레이어와 가스통 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, playerTransform.transform.position);

        // 지정된 거리보다 가까워지면 터진다!
        if (distance <= explosionRadius)
        {
            Explode();
        }
    }

    void Explode()
    {
        isExploded = true;
        Debug.Log("가스통 폭발! 미션 실패.");

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        gameObject.SetActive(false);

        // [수정됨] 시간 초과 대신, 원하는 문구를 직접 쏴줍니다!
        if (missionTimer != null)
        {
            missionTimer.FailMissionWithReason("<color=red>위험 물질 접촉!\n생존자를 구출하지 못했습니다.</color>");
        }
    }

    // 에디터 뷰에서 폭발 범위를 시각적으로 확인하기 위함
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}