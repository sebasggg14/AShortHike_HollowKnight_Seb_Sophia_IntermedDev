using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCode : MonoBehaviour
{
    [SerializeField]
    NextRoom script;

    [SerializeField]
    PlayerEDITS player;

    [SerializeField]
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetTrigger("FadeOut");
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(script.nextScene);
        player.isActive = true;
    }
}
