using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextRoom : MonoBehaviour
{
    // name of the script here
    [SerializeField]
    Player player;

    [SerializeField]
    GameObject transitionObject;

    Animator animator;
    TransitionCode script;

    bool canTransition = false;
    public string nextScene = ""; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canTransition = false;
        animator = transitionObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTransition == true)
        {
            animator.SetTrigger("FadeIn");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        canTransition = false;
        if (collider.CompareTag("NextRoom"))
        {
            nextScene = collider.name;
            StartCoroutine(TransitionDuration());
        }
    }

    IEnumerator TransitionDuration()
    {
        player.isActive = false;
        Debug.Log("start transition");
        yield return new WaitForSeconds(1.5f);
        canTransition = true;
    }
}
