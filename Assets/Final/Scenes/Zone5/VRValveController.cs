using UnityEngine;

public class VRValveController : MonoBehaviour
{
    [Header("연동할 가스 시스템")]
    public ParticleSystem gasParticle; // 가스 연기 파티클
    public AudioSource gasAudio;       // 가스 사운드

    [Header("밸브 회전 연출 세팅")]
    public Transform valveHandle;      // 회전시킬 실제 밸브 손잡이 오브젝트
    public float rotateSpeed = 150f;   // 밸브가 돌아가는 속도
    public float maxRotationAngle = 90f; // 잠겼을 때 도달할 목표 각도

    private bool isClicked = false;     // 레이저 클릭 여부
    private bool isGasStopped = false;  // 가스가 완전히 멈췄는지 여부
    private float accumulatedRotation = 0f;

    void Start()
    {
        // 씬 시작 시 가스와 사운드 켜기
        if (gasParticle != null && !gasParticle.isPlaying) gasParticle.Play();
        if (gasAudio != null && !gasAudio.isPlaying) gasAudio.Play();
    }

    void Update()
    {
        // 1. 레이저 클릭이 되었고, 목표 각도만큼 다 안 돌았다면 회전 처리
        if (isClicked && accumulatedRotation < maxRotationAngle)
        {
            float angleThisFrame = rotateSpeed * Time.deltaTime;

            if (valveHandle != null)
            {
                // 원하셨던 본래의 우회전 코드를 그대로 적용합니다!
                valveHandle.Rotate(-Vector3.forward * angleThisFrame, Space.Self);
            }

            // 돌아간 각도를 차곡차곡 누적
            accumulatedRotation += angleThisFrame;

            // 2. 밸브가 다 돌아간 순간 가스 차단!
            if (accumulatedRotation >= maxRotationAngle && !isGasStopped)
            {
                StopGasSystem();
            }
        }
    }

    // 레이저가 클릭되었을 때 호출되는 함수
    public void TurnOffValve()
    {
        if (isClicked) return; // 중복 클릭 방지
        isClicked = true;
        Debug.Log("밸브 우회전 시작... 끝까지 돌아가면 가스가 차단됩니다.");
    }

    // 실제 가스 시스템을 끄는 독립된 함수
    private void StopGasSystem()
    {
        isGasStopped = true;

        // 가스 연기 파티클 끄기
        if (gasParticle != null) gasParticle.Stop();

        // 가스 소리 끄기
        if (gasAudio != null) gasAudio.Stop();

        Debug.Log("밸브 회전 완료! 가스 및 사운드 차단 성공.");
    }
}