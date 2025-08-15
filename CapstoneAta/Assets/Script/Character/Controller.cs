using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Transform yawRoot; // rotate around Y (assign PlayerCameraRoot)
    public Camera cam;

    [Header("Settings")]
    public float lookSensitivity = 0.15f;
    public float gyroSensitivity = 3.0f;
    public bool gyroEnabled = false;
    public bool invertY = false;

    Vector2 lastTouchPos;
    float pitch = 0f; // camera local X rotation
    float yaw = 0f;   // root Y rotation

    void Start()
    {
        if (cam == null) cam = Camera.main;

        // Enable gyro hardware (does nothing if unsupported)
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // Editor fallback: mouse look
        if (!gyroEnabled && Input.GetMouseButton(1))
        {
            Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            ApplyTouchDelta(delta * 10f * lookSensitivity);
        }
#endif

        if (gyroEnabled && SystemInfo.supportsGyroscope)
        {
            ApplyGyro(); // Gyro aiming only
        }
        else
        {
            HandleTouchLook(); // Touch aiming only
        }

        // Clamp pitch to avoid flipping
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        // Apply rotations
        cam.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        yawRoot.localEulerAngles = new Vector3(0f, yaw, 0f);
    }

    void HandleTouchLook()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began)
        {
            lastTouchPos = t.position;
        }
        else if (t.phase == TouchPhase.Moved)
        {
            Vector2 delta = t.deltaPosition;
            ApplyTouchDelta(delta * lookSensitivity * Time.deltaTime * 60f);
        }
    }

    void ApplyTouchDelta(Vector2 delta)
    {
        float deltaX = delta.x;
        float deltaY = delta.y * (invertY ? 1 : -1);
        yaw += deltaX;
        pitch += deltaY;
    }

    void ApplyGyro()
    {
        Vector3 rotationRate = Input.gyro.rotationRateUnbiased;
        yaw += rotationRate.y * Mathf.Rad2Deg * gyroSensitivity * Time.deltaTime;
        pitch += -rotationRate.x * Mathf.Rad2Deg * gyroSensitivity * Time.deltaTime;
    }

    // Public setter for settings menu
    public void SetGyroEnabled(bool on)
    {
        gyroEnabled = on;
    }
}

