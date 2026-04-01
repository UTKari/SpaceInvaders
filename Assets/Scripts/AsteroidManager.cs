using UnityEngine;
using UnityEngine.Events;
 
public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    private InstantiatePoolObjects asteroidPool;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float spawnInterval = 2f;
    [SerializeField]
    private UnityEvent<Transform> onAsteroidDestroyed;
    private void Start()
    {
        InvokeRepeating("SpawnAsteroid", spawnInterval, spawnInterval);
    }
    private void SpawnAsteroid()
    {
        Vector3 randomDistanceFromTarget = Random.onUnitSphere * 20f;
        randomDistanceFromTarget.y = Mathf.Abs(randomDistanceFromTarget.y) + 5f;
        Vector3 spawnPosition = target.position + randomDistanceFromTarget;
        asteroidPool.InstantiateObject(spawnPosition);
        Asteroid asteroid = asteroidPool.GetCurrentObject().GetComponent<Asteroid>();
        asteroid.SetTarget(target);
        asteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);
        spawnInterval = Random.Range(1f, 3f);
        Invoke("SpawnAsteroid", spawnInterval);
    }
    public void OnAsteroidDestroyed(Transform asteroid)
    {
        onAsteroidDestroyed?.Invoke(asteroid);
    }
}
 