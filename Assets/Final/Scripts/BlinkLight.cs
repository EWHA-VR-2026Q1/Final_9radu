using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    public Light warningLight;

    public float blinkSpeed = 0.5f;

    private bool isOn = true;

    void Start()
    {
        InvokeRepeating("ToggleLight", 0f, blinkSpeed);
    }

    void ToggleLight()
    {
        isOn = !isOn;
        warningLight.enabled = isOn;
    }
}