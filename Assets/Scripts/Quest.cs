using UnityEngine;

public class Quest : MonoBehaviour
{
    private Transform wBox;
    private Transform mainBox;
    private Transform completeBox;
    public bool questComplete = false;

    public static bool showAudioForFirsttime = false;

    AudioSource aud;
    public AudioClip defaultNoise;
    public AudioClip completionNoise;
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
                    var go = new GameObject("SFX_Collect");
                    go.transform.position = Camera.main.transform.position; // play at listener so it's always loud
                    var src = go.AddComponent<AudioSource>();
                    src.clip = defaultNoise;
                    src.spatialBlend = 0f;   // 2D
                    src.volume = 1f;
                    src.Play();
                    Destroy(go, defaultNoise.length);
                }
                else //prompt completed quest dialogue
                {
                    //show 
                    mainBox.gameObject.SetActive(false); 
                    wBox.gameObject.SetActive(false); //hide w option
                    ObjectivesTracker.questComplete = true; 
                    completeBox.gameObject.SetActive(true);

                    if (showAudioForFirsttime)
                    {
                        var go = new GameObject("SFX_Collect");
                        go.transform.position = Camera.main.transform.position; // play at listener so it's always loud
                        var src = go.AddComponent<AudioSource>();
                        src.clip = completionNoise;
                        src.spatialBlend = 0f;   // 2D
                        src.volume = 1f;
                        src.Play();
                        Destroy(go, completionNoise.length);
                        showAudioForFirsttime = false;
                    }
                    
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
