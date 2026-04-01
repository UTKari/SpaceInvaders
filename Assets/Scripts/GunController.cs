using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private Camera playerCamera;
    private int gunIndex = 0;
    private void Start()
    {
        foreach (Gun gun in guns)
        {
            gun.Camera = playerCamera;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            guns[gunIndex].Shoot();
            gunIndex++;
            if (gunIndex >= guns.Length)
            {
                gunIndex = 0;
            }
        }
    }
}
