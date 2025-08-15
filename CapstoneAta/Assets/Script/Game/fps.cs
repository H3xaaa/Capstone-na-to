using UnityEngine;

public class fps : MonoBehaviour
{
    public int targetFPS = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
    }


}
