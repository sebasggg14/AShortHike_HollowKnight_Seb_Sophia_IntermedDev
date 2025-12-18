using UnityEngine;

public class ObjectivesTracker : MonoBehaviour
{
    public static int enemiesKilled = 0;
    public static bool questComplete = false;

    public AudioClip bgMusic;

    private AudioSource src;

    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.clip = bgMusic;
        src.spatialBlend = 0f; // 2D (THIS is why it's loud)
        src.volume = 1f;
        src.loop = true;
        src.Play();
    }
}