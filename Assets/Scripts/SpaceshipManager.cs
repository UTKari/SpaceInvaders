using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpaceshipManager : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private int numberOfSpaceships = 5;
    [SerializeField]
    private InstantiatePoolObjects spaceshipPool;
    [SerializeField]
    private InstantiatePoolObjects bulletPool;
    [SerializeField]
    private UnityEvent onInstantiateShip;
    [SerializeField]
    private float timeToSpawn = 15f;
    [SerializeField]
    private UnityEvent<Transform> onShipDestroyed;
    [SerializeField]
    private UnityEvent onAllShipsDestroyed;
    private int destroyedSpaceships = 0;
    public void OnDestroyShip(Transform transform)
    {
        destroyedSpaceships++;
        onShipDestroyed.Invoke(transform);
        if (destroyedSpaceships >= numberOfSpaceships)
        {
            onAllShipsDestroyed?.Invoke();
        }
    }
    public void StartShips()
    {
        StartCoroutine(SpawnSpaceships());
    }
    public void StopShips()
    {
        StopAllCoroutines();
        spaceshipPool.DeactivateAllObjects();
    }

    private IEnumerator SpawnSpaceships()
    {
        numberOfSpaceships--;
        yield return new WaitForSeconds(timeToSpawn);
        onInstantiateShip?.Invoke();
        spaceshipPool.InstantiateObject(transform);
        EnemySpaceship spaceship = spaceshipPool.GetCurrentObject().GetComponent<EnemySpaceship>();
        spaceship.TargetHealth = playerHealth;
        spaceship.BulletPool = bulletPool;
        spaceship.OnDestroyed.AddListener(OnDestroyShip);
        if (numberOfSpaceships > 0)
        {
            StartCoroutine(SpawnSpaceships());
        }
    }
}
