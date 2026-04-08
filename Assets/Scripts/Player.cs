using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator damageFade;
    public void TakeDamage()
    {
        damageFade.Play("FadeIn", 0, 0f);
    }
}
