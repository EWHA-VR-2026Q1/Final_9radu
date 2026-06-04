using UnityEngine;
using System.Collections.Generic;

public class Zone4DebrisManager : MonoBehaviour
{
    // 자동으로 찾아낸 잔해들의 Rigidbody 리스트
    private List<Rigidbody> allDebris = new List<Rigidbody>();

    void Start()
    {
        // [핵심] 이 스크립트가 붙은 오브젝트 하위(자식)의 모든 Rigidbody를 자동으로 다 긁어모읍니다.
        GetComponentsInChildren<Rigidbody>(true, allDebris);

        // 게임 시작 시점에는 모든 잔해들이 공중에 멈춰있도록 'Kinematic'을 켜줍니다.
        foreach (var rb in allDebris)
        {
            if (rb != null)
            {
                rb.isKinematic = true; // 락(Lock) 걸기! 중력이나 힘의 영향을 받지 않고 가만히 고정됨
            }
        }
        Debug.Log($"총 {allDebris.Count}개의 잔해 고정 완료! 미션 시작을 대기합니다.");
    }

    // [핵심 호출 함수] 미션 Start 버튼이나 미션 시작 트리거에서 이 함수를 호출하면 됩니다!
    public void ReleaseDebris()
    {
        foreach (var rb in allDebris)
        {
            if (rb != null)
            {
                rb.isKinematic = false; // 락 해제! 이제 중력이 적용되어 아래로 떨어지기 시작합니다.
            }
        }
        Debug.Log("미션 스타트! 모든 잔해가 떨어지기 시작합니다!");
    }
}