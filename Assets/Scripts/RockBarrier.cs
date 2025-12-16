using UnityEngine;

public class RockBarrier : MonoBehaviour
{
    private Transform completeBox;
    public bool questComplete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        completeBox = transform.Find("barrier text");
        if (completeBox != null) {
            // Start hidden
            completeBox.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !ObjectivesTracker.questComplete)
        {
           completeBox.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           //hide all dialogue
            completeBox.gameObject.SetActive(false);
        }
    }
}
