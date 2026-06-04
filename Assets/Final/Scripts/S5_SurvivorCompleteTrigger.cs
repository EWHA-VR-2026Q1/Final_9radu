using UnityEngine;
using TMPro;

public class S5SurvivorCompleteTrigger : MonoBehaviour
{
    public TextMeshProUGUI missionText;
    public TextMeshProUGUI completeText;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (missionText != null)
            {
                missionText.text = "임무 완료\n생존자 발견";
            }

            if (completeText != null)
            {
                completeText.text = "탈출 경로 확보";
            }
        }
    }
}