using UnityEngine;

public class Quest : MonoBehaviour
{
    private Transform wBox;
    private Transform mainBox;
    private Transform completeBox;
    public bool questComplete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wBox = transform.Find("Char textbox");
        mainBox = transform.Find("main text");
        completeBox = transform.Find("complete text");
        if (wBox != null && mainBox != null && completeBox != null) {
            // Start hidden
            wBox.gameObject.SetActive(false); 
            mainBox.gameObject.SetActive(false); 
            completeBox.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectivesTracker.enemiesKilled == 3)
        {
            questComplete = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                //prompt initial quest dialogue
                if (!questComplete)
                {
                    //show 
                    mainBox.gameObject.SetActive(true); 
                    wBox.gameObject.SetActive(false); //hide w option
                    completeBox.gameObject.SetActive(false);
                }
                else //prompt completed quest dialogue
                {
                    //show 
                    mainBox.gameObject.SetActive(false); 
                    wBox.gameObject.SetActive(false); //hide w option
                    completeBox.gameObject.SetActive(true);
                }
    
            }
            else
            {
                //show 'press w' option
                if (wBox != null) {
                    wBox.gameObject.SetActive(true); // Unhide the child
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                //prompt initial quest dialogue
                if (!questComplete)
                {
                    //show 
                    mainBox.gameObject.SetActive(true); 
                    wBox.gameObject.SetActive(false); //hide w option
                    completeBox.gameObject.SetActive(false);
                }
                else //prompt completed quest dialogue
                {
                    //show 
                    mainBox.gameObject.SetActive(false); 
                    wBox.gameObject.SetActive(false); //hide w option
                    completeBox.gameObject.SetActive(true);
                }
            }
        }
    }

    

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           //hide all dialogue
            wBox.gameObject.SetActive(false); 
            mainBox.gameObject.SetActive(false); 
            completeBox.gameObject.SetActive(false);
        }
    }
}
