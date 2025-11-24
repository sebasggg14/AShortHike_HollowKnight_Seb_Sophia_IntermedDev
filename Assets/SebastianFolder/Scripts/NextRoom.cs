using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextRoom : MonoBehaviour
{
    // name of the script here
    [SerializeField]
    PlayerEDITS player;

    bool canTransition = false;
    string nextScene = ""; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canTransition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTransition == true)
        {
            LoadSceneByName(nextScene);
            player.isActive = true;
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

    void LoadSceneByName (string name)
    {
        SceneManager.LoadScene(name);
    }

    IEnumerator TransitionDuration()
    {
        player.isActive = false;
        Debug.Log("start transition");
        yield return new WaitForSeconds(1.5f);
        canTransition = true;
    }
}
