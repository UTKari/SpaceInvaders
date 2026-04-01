using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private Rigidbody rb;
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        rb.linearVelocity = transform.forward * speed;
        trailRenderer.Clear();
    }
    private void StopBullet()
    {
        trailRenderer.Clear();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider collision)
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        StopBullet();
    }
}
