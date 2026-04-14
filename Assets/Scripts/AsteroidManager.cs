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
    [SerializeField]
    private int numberOfAsteroids = 10;
    [SerializeField]
    private UnityEvent onAllAsteroidsDestroyed;
    [SerializeField]
    private UnityEvent onInstantiateAsteroid;
    private int asteroidsDestroyed = 0;
    private bool isActive = true;
    public void StartAsteroids()
    {
        float iniialDelay = 0f;
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Invoke("SpawnAsteroid", iniialDelay);
            iniialDelay += spawnInterval;
        }
    }
    public void StopAsteroids()
    {
        isActive = false;
        CancelInvoke("SpawnAsteroid");
        asteroidPool.DeactivateAllObjects();
    }
    private void SpawnAsteroid()
    {
        if (!isActive) return;
        Vector3 randomDistanceFromTarget = Random.onUnitSphere * 20f;
        randomDistanceFromTarget.y = Mathf.Abs(randomDistanceFromTarget.y) + 5f;
        Vector3 spawnPosition = target.position + randomDistanceFromTarget;
        asteroidPool.InstantiateObject(spawnPosition);
        Asteroid asteroid = asteroidPool.GetCurrentObject().GetComponent<Asteroid>();
        asteroid.SetTarget(target);
        asteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);
        onInstantiateAsteroid?.Invoke();
    }
    public void OnAsteroidDestroyed(Transform asteroid)
    {
        asteroidsDestroyed++;
        if (asteroidsDestroyed >= numberOfAsteroids)
            onAllAsteroidsDestroyed?.Invoke();
        onAsteroidDestroyed?.Invoke(asteroid);
    }
}
 