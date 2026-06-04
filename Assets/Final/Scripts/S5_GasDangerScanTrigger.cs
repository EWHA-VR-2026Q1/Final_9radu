using UnityEngine;
using TMPro;

public class S5GasDangerScanTrigger : MonoBehaviour
{
    public TextMeshProUGUI dangerHUD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dangerHUD != null)
            {
                dangerHUD.text = "위험 물체 감지\n스캔 실패";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dangerHUD != null)
            {
                dangerHUD.text = "";
            }
        }
    }
}