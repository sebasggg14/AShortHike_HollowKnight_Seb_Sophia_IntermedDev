using UnityEngine;

public class Party : MonoBehaviour
{

    private Transform wBox;
    private Transform mainBox;

    AudioSource aud;
    public AudioClip defaultNoise;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wBox = transform.Find("Char textbox");
        mainBox = transform.Find("main text");
        if (wBox != null && mainBox != null) {
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
        if (other.CompareTag("Player") && Mom.walk)
        {
            if (Input.GetKey(KeyCode.W))
            {
                mainBox.gameObject.SetActive(true); 
                wBox.gameObject.SetActive(false); //hide w option
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
        if (other.CompareTag("Player") && Mom.walk)
        {
            if (Input.GetKey(KeyCode.W))
            {
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
            mainBox.gameObject.SetActive(false); 
            wBox.gameObject.SetActive(false);
        }
    }
}
