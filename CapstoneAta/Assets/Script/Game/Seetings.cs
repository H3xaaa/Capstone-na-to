using UnityEngine;
using UnityEngine.UI;

public class Seetings : MonoBehaviour
{
    [Header("UI References")]
    public Toggle gyroToggle; // Assign your Toggle here

    [Header("Player Controller")]
    public Controller playerController; // Assign your Controller script here

    void Start()
    {
        // Load saved value (default off)
        bool gyroOn = PlayerPrefs.GetInt("GyroEnabled", 0) == 1;

        // Set UI toggle without triggering OnValueChanged event
        gyroToggle.isOn = gyroOn;

        // Apply to player controller
        if (playerController != null)
        {
            playerController.SetGyroEnabled(gyroOn);
        }

        // Subscribe to toggle change
        gyroToggle.onValueChanged.AddListener(OnGyroToggleChanged);
    }

    void OnGyroToggleChanged(bool on)
    {
        if (playerController != null)
        {
            playerController.SetGyroEnabled(on);
        }
    }
}