using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemySpaceship : MonoBehaviour
{
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private float playerDamage =10f;
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private string[] flyAnimationNames;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Health targetHealth;
    [SerializeField]
    private Health health;
    [SerializeField]
    private UnityEvent<Transform> onDestroyed;
    public UnityEvent<Transform> OnDestroyed => onDestroyed;
    private Coroutine animationCoroutine;
    public Health TargetHealth
    {
        set
        {
            targetHealth = value;
        }
    }
    [SerializeField]
    private InstantiatePoolObjects bulletPool;
    public InstantiatePoolObjects BulletPool
    {
        set
        {
            bulletPool = value;
        }
    }
    private void OnEnable()
    {
        health.InitializeHealth();
        int randomIndex = Random.Range(0, flyAnimationNames.Length);
        animator.Play(flyAnimationNames[randomIndex], 0, 0f);
        animationCoroutine = StartCoroutine(AnimationCoroutine());
    }
    private IEnumerator AnimationCoroutine()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    public void OnPointerClick()
    {
        health.TakeDamage(playerDamage);
    }
    public void DestroySpaceship()
    {
        //onDestroyed.Invoke(transform);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        targetHealth = null;
        onDestroyed.Invoke(transform);
        onDestroyed.RemoveAllListeners();
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    public void ShootTarget()
    {
        if (targetHealth != null)
        {
            bulletPool.InstantiateObject(transform.position);
            Vector3 direction = (targetHealth.transform.position - transform.position).normalized;
            bulletPivot.forward = direction;
            Transform bullet = bulletPool.GetCurrentObject().transform;
            bullet.transform.LookAt(targetHealth.transform.position);
            targetHealth.TakeDamage(damage);
            SoundManager.instance.Play("laser");
        }
    }
}
