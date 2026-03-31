using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField]
    private float secondsToDestroy = 1f;
    private void OnEnable()
    {
        Invoke(nameof(DestroySelf), secondsToDestroy);
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}
