using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Color targetColor;

    void Start()
    {
        GetComponent<Renderer>().material.color = targetColor;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
