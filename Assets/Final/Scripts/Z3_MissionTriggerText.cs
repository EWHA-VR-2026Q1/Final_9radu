using UnityEngine;
using TMPro;

public class MissionTriggerText : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public string message = "MISSION COMPLETE\nSURVIVOR LOCATED";

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (targetText != null)
            {
                targetText.text = message;
            }
        }
    }
}