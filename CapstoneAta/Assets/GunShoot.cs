using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public float range = 50f;
    public float damage = 10f;
    public ParticleSystem muzzleFlash;

    public void ShootButtonPressed()
    {
        Shoot();
    }

    void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}