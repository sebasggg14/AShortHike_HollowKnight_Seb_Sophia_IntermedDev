using UnityEngine;

public class featherOBJ : MonoBehaviour
{
    AudioSource aud;
    public AudioClip collection;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var go = new GameObject("SFX_Collect");
            go.transform.position = Camera.main.transform.position; // play at listener so it's always loud
            player.maxFeathers++;
            player.health++;
            var src = go.AddComponent<AudioSource>();
            src.clip = collection;
            src.spatialBlend = 0f;   // 2D
            src.volume = 1f;
            src.Play();
            Destroy(go, collection.length);
            Destroy(gameObject);
        }
    }
}
