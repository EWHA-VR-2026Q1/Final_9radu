using UnityEngine;

public class AutoTeleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;
    public string playerTag = "Player";
    public bool oneTimeOnly = true;

    [Header("Optional Effects")]
    public AudioClip teleportSound;
    public GameObject teleportVFX;

    bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (oneTimeOnly && hasTriggered) return;

        // 플레이어 감지 (태그 또는 이름으로)
        if (other.CompareTag(playerTag) || other.transform.root.name.Contains("Player"))
        {
            Teleport(other.transform.root);
        }
    }

    void Teleport(Transform player)
    {
        if (teleportTarget == null)
        {
            Debug.LogWarning("[AutoTeleporter] teleportTarget이 비어있습니다.");
            return;
        }

        // CharacterController가 있으면 잠시 비활성화 (위치 충돌 방지)
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // 플레이어 위치 이동
        player.position = teleportTarget.position;

        // 효과음 + VFX
        if (teleportSound != null)
            AudioSource.PlayClipAtPoint(teleportSound, teleportTarget.position);
        if (teleportVFX != null)
            Instantiate(teleportVFX, teleportTarget.position, Quaternion.identity);

        if (cc != null) cc.enabled = true;
        hasTriggered = true;

        Debug.Log("[AutoTeleporter] 텔레포트 완료");
    }
}