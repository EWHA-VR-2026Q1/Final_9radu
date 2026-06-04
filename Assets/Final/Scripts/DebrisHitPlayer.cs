using UnityEngine;
using TMPro;

public class DebrisHitPlayer : MonoBehaviour
{
    public TextMeshProUGUI warningText;

    private int health = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;

            if (warningText != null)
            {
                warningText.text = "HIT BY DEBRIS!\nHEALTH: " + health;
            }

            if (health <= 0 && warningText != null)
            {
                warningText.text = "MISSION FAILED\nTOO MANY HITS";
            }
        }
    }
}