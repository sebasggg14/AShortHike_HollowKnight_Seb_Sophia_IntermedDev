using UnityEngine;

public class abyss : MonoBehaviour
{
    AudioSource aud;
    public AudioClip damage;
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
           //reset player
           player.health--;
           aud.PlayOneShot(damage);
           other.transform.position = new Vector3(-1340.98f, 8.18f, 1.55f);
        }
    }
}
