using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCode : MonoBehaviour
{
    [SerializeField]
    NextRoom script;

    [SerializeField]
    Player player;

    [SerializeField]
    Animator animator;

    public void PlayFadeOut() 
    {
        animator.ResetTrigger("FadeIn");
        animator.SetTrigger("FadeOut");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayFadeOut(); // fade out of black square and into scene
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(script.nextScene);
        player.isActive = true;
    }
}
