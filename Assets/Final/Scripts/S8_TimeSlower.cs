using UnityEngine;
using System.Collections.Generic;

public class Zone4DebrisSlower : MonoBehaviour
{
    [Header("[설정] 잔해들을 얼마나 느리게 만들 건가요? (0.1 ~ 0.3 추천)")]
    public float slowFactor = 0.2f;

    // 현재 슬로우 구역 안에 들어와 있는 잔해들의 Rigidbody를 저장하는 리스트
    private List<Rigidbody> debrisInZone = new List<Rigidbody>();

    // 각 잔해물이 구역에 처음 들어왔을 때의 원래 속도를 기억할 딕셔너리
    private Dictionary<Rigidbody, Vector3> initialVelocities = new Dictionary<Rigidbody, Vector3>();

    void FixedUpdate()
    {
        for (int i = debrisInZone.Count - 1; i >= 0; i--)
        {
            Rigidbody rb = debrisInZone[i];

            // 1. 그 사이에 파괴되거나 없어진 잔해 예외 처리
            if (rb == null || !rb.gameObject.activeInHierarchy)
            {
                if (rb != null) initialVelocities.Remove(rb);
                debrisInZone.RemoveAt(i);
                continue;
            }

            //  [철통 방어] 혹시라도 플레이어가 리스트에 끼어 들어왔다면 감속하지 않고 즉시 리스트에서 퇴출!
            if (rb.CompareTag("Player") || rb.name.Contains("Player") || rb.name.Contains("OVR"))
            {
                debrisInZone.RemoveAt(i);
                continue;
            }

            Vector3 currentVel = rb.velocity;

            // 기존 속도의 방향은 유지하되, 크기를 강제로 '원래 속도 * slowFactor' 또는 초당 아주 낮은 고정 수치로 제한
            if (currentVel.magnitude > 0.1f)
            {
                rb.velocity = currentVel.normalized * Mathf.Min(currentVel.magnitude, 1.5f) * slowFactor;
            }

            // 회전 속도도 팽이처럼 돌지 않게 꽉 억제
            rb.angularVelocity = rb.angularVelocity * slowFactor;
        }
    }

    //  오브젝트가 슬로우 구역 박스 안으로 진입했을 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Player") ||
            other.name.Contains("OVR") || other.name.Contains("Hand"))
        {
            return; // 플레이어 계열은 무시
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (!debrisInZone.Contains(rb))
            {
                debrisInZone.Add(rb);
                // 진입 순간의 속도를 기억
                initialVelocities[rb] = rb.velocity;

                //  진입하자마자 속도를 일단 확 줄여버려서 충격을 완화함!
                rb.velocity *= slowFactor;
            }
        }
    }

    //  오브젝트가 구역을 벗어날 때 (원래 속도로 복구)
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Collider>().attachedRigidbody;
        if (rb != null && debrisInZone.Contains(rb))
            if (rb != null && debrisInZone.Contains(rb))
            {
                debrisInZone.Remove(rb);
                initialVelocities.Remove(rb);
            }
    }
}