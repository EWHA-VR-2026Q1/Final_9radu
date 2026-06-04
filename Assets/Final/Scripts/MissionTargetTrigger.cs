using UnityEngine;
using UnityEngine.Events;

public class MissionTargetTrigger : MonoBehaviour
{
    [Header("[설정] 플레이어가 이 물체와 접촉할 때 실행할 미션 성공 함수")]
    public UnityEvent onMissionSuccess;

    [Header("[옵션] 성공 시 이 물체를 사라지게 할까요? (예: 생존자 구조 후 사라짐)")]
    public bool destroyOnSuccess = false;

    private bool isActivated = false;

    // 플레이어의 손(컨트롤러)이나 몸이 이 실제 오브젝트에 닿았을 때!
    private void OnTriggerEnter(Collider other)
    {
        // 이미 미션이 성공했으면 중복 실행 방지
        if (isActivated) return;

        // 플레이어 구별 (태그가 Player이거나, OVR 카메라릭, 혹은 Hand가 이름에 포함된 경우)
        if (other.CompareTag("Player") || other.name.Contains("Player") || other.name.Contains("Hand") || other.name.Contains("OVR"))
        {
            isActivated = true;
            Debug.Log($" [미션 성공 트리거 발동]: {gameObject.name} 오브젝트 터치 완료!");

            //  인스펙터 창에서 등록해 둔 성공 함수(예: MissionTimer의 CompleteMission 등)를 실행!
            if (onMissionSuccess != null)
            {
                onMissionSuccess.Invoke();
            }

            // 아이템 획득이나 구조 연출처럼 오브젝트가 사라져야 하는 경우
            if (destroyOnSuccess)
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}