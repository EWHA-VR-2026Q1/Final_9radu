using UnityEngine;

public class S6_ScanBeepTrigger : MonoBehaviour
{
    public AudioSource scanBeep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scanBeep != null)
            {
                scanBeep.Play();
            }
        }
    }
}