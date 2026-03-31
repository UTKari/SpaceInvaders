using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onSceneStart;
    [SerializeField]
    private Animator fadeanimator;
    private void  Start()
    {
        onSceneStart?.Invoke();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeCoroutine(sceneName));
    }

    private IEnumerator FadeCoroutine(string sceneName)
    {
        fadeanimator.Play("FadeOut", 0, 0f);
        yield return null;
        yield return new WaitForSeconds(fadeanimator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneName);
    }
}
