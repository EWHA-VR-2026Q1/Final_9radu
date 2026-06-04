using UnityEngine;

public class PlayerFootstepController : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("[설정] 발소리가 켜지는 조이스틱 최소 입력값")]
    public float moveThreshold = 0.1f;

    void Start()
    {
        // 1. 같은 오브젝트에 붙어있는 Audio Source를 자동으로 가져옴
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 2. 메타 퀘스트 왼쪽 컨트롤러의 프라이머리 썸스틱(이동 조이스틱) 입력값 가져오기
        // (만약 오른쪽 스틱으로 이동한다면 SecondaryThumbstick으로 변경)
        Vector2 inputAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // 3. 조이스틱의 움직임 크기(Magnitude) 계산
        float moveSpeed = inputAxis.magnitude;

        // 4. 플레이어가 조이스틱을 일정 크기 이상으로 밀었을 때 (움직일 때)
        if (moveSpeed > moveThreshold)
        {
            // 소리가 이미 재생 중이 아니라면 재생 시작!
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log(" 플레이어 이동 중: 발소리 재생");
            }
        }
        else // 조이스틱을 놓고 가만히 서 있을 때
        {
            // 소리가 재생 중이라면 일시 정지 또는 정지!
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // 또는 audioSource.Pause();
                Debug.Log(" 플레이어 정지: 발소리 멈춤");
            }
        }
    }
}