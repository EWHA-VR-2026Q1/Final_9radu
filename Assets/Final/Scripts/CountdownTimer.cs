using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 10f;

    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            timerText.text = "TIME LIMIT: 00:00\nMISSION FAILED";
            return;
        }

        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "TIME LIMIT: 00:" + seconds.ToString("00") + "\nESCAPE BEFORE COLLAPSE";
    }
}