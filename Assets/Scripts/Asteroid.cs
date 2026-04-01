using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private UnityEvent<Transform> onAsteroidDestroyed;
    public UnityEvent<Transform> OnAsteroidDestroyed => onAsteroidDestroyed;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    public void DestroyAsteroid()
    {
        animator.Play("Destroy", 0, 0f);
        onAsteroidDestroyed?.Invoke(transform);
        StartCoroutine(DestroyCoroutine());
    }
    private IEnumerator DestroyCoroutine()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        target = null;
        onAsteroidDestroyed.RemoveAllListeners();
    }
}
