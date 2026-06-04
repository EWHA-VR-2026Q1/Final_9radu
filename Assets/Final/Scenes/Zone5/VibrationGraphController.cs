using UnityEngine;
using System.Collections.Generic;

public class VibrationGraphController : MonoBehaviour
{
    [Header("거리에 따라 늘어날 대분류 칸 (1, 2, 3, 4, 5, 6 넣는 곳)")]
    public List<GameObject> batteryStages = new List<GameObject>();

    [Header("거리 감지 설정 (오디오 싱크 맞춤)")]
    public Transform playerCamera;
    [Tooltip("가스 소리가 들리기 시작하는 가장 먼 거리 (기본 35m)")]
    public float detectionRadius = 35f;
    [Tooltip("가스 소리가 최대가 되는 코앞 거리 (기본 3m)")]
    public float minDistance = 3f;

    [Header("눈금 요동치기 속도 (숫자가 클수록 빠르게 파르르 바뀜)")]
    public float waveSpeed = 15f;

    private bool isPermanentlyDisabled = false;
    private List<List<GameObject>> childCubesList = new List<List<GameObject>>();
    private float timer = 0f;

    void Start()
    {
        // 1. 이름이나 하이어라키 순서가 꼬여있어도 강제로 정렬하여 인식하는 초기화 로직
        for (int i = 0; i < batteryStages.Count; i++)
        {
            List<GameObject> stageChildren = new List<GameObject>();

            if (batteryStages[i] != null)
            {
                // 부모 폴더(1~6) 밑에 붙어있는 모든 자식 눈금들을 싹 쓸어담습니다.
                foreach (Transform child in batteryStages[i].transform)
                {
                    stageChildren.Add(child.gameObject);
                }

                // [중요] 눈금들의 실제 화면상 가로(X) 좌표를 계산해서 왼쪽->오른쪽 순서로 재정렬합니다.
                stageChildren.Sort((a, b) => a.transform.localPosition.x.CompareTo(b.transform.localPosition.x));

                // 시작할 때는 모든 작은 눈금들을 꺼둡니다.
                foreach (GameObject cube in stageChildren)
                {
                    cube.SetActive(false);
                }

                // 1~6번 기둥 틀(부모) 자체는 항상 켜둡니다.
                batteryStages[i].SetActive(true);
            }

            childCubesList.Add(stageChildren);
        }

        // 2. 만약 인스펙터 서랍에 카메라를 깜빡하고 안 넣었다면, 오큘러스의 진짜 눈(CenterEyeAnchor)을 자동 추적합니다.
        if (playerCamera == null || playerCamera.name == "Player_OVRInput_Combined")
        {
            GameObject realCamera = GameObject.Find("CenterEyeAnchor");
            if (realCamera != null)
            {
                playerCamera = realCamera.transform;
                Debug.Log("🎯 [VibrationHUD] 진짜 VR 카메라(CenterEyeAnchor)를 자동으로 찾아 연결했습니다.");
            }
            else if (Camera.main != null)
            {
                playerCamera = Camera.main.transform;
            }
        }
    }

    void Update()
    {
        // 영구 정지되었거나 필수 서랍이 비어있으면 연산을 수행하지 않습니다.
        if (isPermanentlyDisabled || playerCamera == null || batteryStages.Count == 0) return;

        // 3. 로컬 좌표계의 함정에 빠지지 않도록 부모의 위치를 무시한 '진짜 월드 절대 좌표' 기준 거리를 계산합니다.
        Vector3 playerWorldPos = playerCamera.position;
        Vector3 valveWorldPos = this.transform.position;

        float distance = Vector3.Distance(playerWorldPos, valveWorldPos);

        // 오디오 최대 도달 거리(35m) 안으로 진입했을 때부터 연출 시작
        if (distance <= detectionRadius)
        {
            // 거리를 35m~3m 사이의 0~1 비율로 변환 (가까울수록 1에 가까워짐)
            float lerpValue = (detectionRadius - distance) / (detectionRadius - minDistance);
            lerpValue = Mathf.Clamp01(lerpValue);

            // waveSpeed에 맞춰 일정한 간격으로 랜덤 눈금 개수 갱신 (렉 방지 및 연출 최적화)
            timer += Time.deltaTime * waveSpeed;
            if (timer >= 1f)
            {
                timer = 0f;

                // 유니티 하단 콘솔(Console) 탭에 실시간 진짜 월드 거리가 찍히게 만듭니다.
                Debug.Log($"[HUD] 밸브와의 실제 거리: {distance:F2}m / 게이지 출력 세기: {lerpValue * 100:F0}%");

                for (int i = 0; i < batteryStages.Count; i++)
                {
                    List<GameObject> children = childCubesList[i];
                    int totalCubes = children.Count;

                    if (totalCubes == 0) continue;

                    // 거리에 비례하여 현재 활성화될 수 있는 최소/최대 눈금 상한선 계산
                    int maxCubesCount = Mathf.CeilToInt(lerpValue * totalCubes);
                    int minCubesCount = Mathf.FloorToInt(lerpValue * (totalCubes * 0.3f));

                    // 35m 근처 극초반 범위에서는 최소 1~2개 수준만 튀도록 방지
                    if (maxCubesCount < 2) maxCubesCount = 2;

                    // 6개의 기둥(1~6)이 각자 완전히 독립적으로 들쑥날쑥 요동치도록 개별 랜덤값을 뽑습니다.
                    int randomActiveCount = Random.Range(minCubesCount, maxCubesCount + 1);
                    randomActiveCount = Mathf.Clamp(randomActiveCount, 0, totalCubes);

                    // X좌표로 예쁘게 줄 세워둔 눈금들을 정해진 랜덤 개수만큼 왼쪽부터 활성화합니다.
                    for (int j = 0; j < totalCubes; j++)
                    {
                        if (children[j] != null)
                        {
                            children[j].SetActive(j < randomActiveCount);
                        }
                    }
                }
            }
        }
        else
        {
            // 35m 범위를 완전히 벗어나면 모든 눈금을 보이지 않게 끕니다.
            HideAllCubes();
        }
    }

    // 모든 자식 눈금들을 한 번에 숨기는 편의용 함수
    private void HideAllCubes()
    {
        for (int i = 0; i < childCubesList.Count; i++)
        {
            foreach (GameObject child in childCubesList[i])
            {
                if (child != null) child.SetActive(false);
            }
        }
    }

    // [밸브 잠금 연동 전용 함수] VRValveController.cs 에서 최종 차단 시 이 함수를 호출해줍니다.
    public void DisableGraphPermanently()
    {
        isPermanentlyDisabled = true;
        HideAllCubes();

        // 밸브가 완벽히 잠기면 요동치던 게이지를 싹 끄고, 
        // 1번 기둥의 가장 첫 번째(맨 왼쪽) 작은 눈금 딱 1개만 안전 안내 잔상용 실선으로 은은하게 켜둡니다.
        if (childCubesList.Count > 0 && childCubesList[0].Count > 0)
        {
            if (childCubesList[0][0] != null) childCubesList[0][0].SetActive(true);
        }

        this.enabled = false;
    }
}