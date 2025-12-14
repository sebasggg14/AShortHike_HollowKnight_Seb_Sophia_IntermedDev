using UnityEngine;

public class Quest : MonoBehaviour
{
    private Transform wBox;
    private Transform mainBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wBox = transform.Find("Char textbox");
        mainBox = transform.Find("main text");
        if (wBox != null) {
            // Start hidden
            wBox.gameObject.SetActive(false); 
            mainBox.gameObject.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                //prompt dialogue
                 mainBox.gameObject.SetActive(true); 
                 wBox.gameObject.SetActive(false); //hide w option
            }
            else
            {
                //show 'press q' option
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
                //prompt dialogue
                 mainBox.gameObject.SetActive(true); 
                 wBox.gameObject.SetActive(false); //hide w option
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
        }
    }
}
