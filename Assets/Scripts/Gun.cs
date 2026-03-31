using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera cameraUsed;
    public Camera Camera
    {
        set{cameraUsed = value;}
    }
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private InstantiatePoolObjects bulletPool;
    private float rayDistance =1000f;
    public void Shoot()
    {
        Ray ray = cameraUsed.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * rayDistance;
        }
        Vector3 direction = (targetPoint - transform.position).normalized;
        bulletPivot.forward = direction;
        bulletPool.InstantiateObject(bulletPivot.position);
        Transform bullet = bulletPool.GetCurrentObject().transform;
        bullet.transform.LookAt(targetPoint);
    }
}
