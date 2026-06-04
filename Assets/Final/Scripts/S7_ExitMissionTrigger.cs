using UnityEngine;
using TMPro;

public class S7_ExitMissionTrigger : MonoBehaviour
{
    public TextMeshProUGUI targetText;

    public CountdownTimer countdownTimer;

    private bool completed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (completed) return;

        if (other.CompareTag("Player"))
        {
            completed = true;

            if (countdownTimer != null)
            {
                countdownTimer.enabled = false;
            }

            if (targetText != null)
            {
                targetText.text =
                    "MISSION COMPLETE\nESCAPE SUCCESSFUL";
            }
        }
    }
}