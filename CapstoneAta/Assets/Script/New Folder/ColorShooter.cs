using UnityEngine;
using UnityEngine.UI;

public class ColorShooter : MonoBehaviour
{
    public Camera cam; // your main camera
    public Button shootButton;
    public Button yellowButton, redButton, blueButton;

    [Header("Customizable Colors")]
    public Color yellowColor = Color.yellow;
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;

    private Color selectedColor = Color.white;

    void Start()
    {
        // Assign button listeners
        yellowButton.onClick.AddListener(() => SelectColor(yellowColor));
        redButton.onClick.AddListener(() => SelectColor(redColor));
        blueButton.onClick.AddListener(() => SelectColor(blueColor));

        shootButton.onClick.AddListener(Shoot);
    }

    void SelectColor(Color color)
    {
        selectedColor = color;
        Debug.Log("Selected color: " + color);
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // from crosshair center
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = selectedColor;
                Debug.Log("Blob hit! Changed color.");
            }
        }
    }
}
