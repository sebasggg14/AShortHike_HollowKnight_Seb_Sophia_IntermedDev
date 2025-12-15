using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class Rock : MonoBehaviour
{
    private AudioSource aud;

    [Header("Audio")]
    public AudioClip breaking;

    [Header("Effects")]
    public GameObject breakParticlesPrefab;   // assign in Inspector

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.playOnAwake = false;
        aud.spatialBlend = 0f;   // 2D
        aud.volume = 1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (ObjectivesTracker.questComplete) {
            if (!other.CompareTag("Stick")) return;
            if (Player.canAttack) return;

            // --- Play sound ---
            if (breaking != null)
                aud.PlayOneShot(breaking, 2f);

            // --- Spawn particles ---
            if (breakParticlesPrefab != null)
            {
            Vector3 spawnOffset = new Vector3(0f, 15f, 1f);
                Vector3 spawnPos = transform.position + spawnOffset;

                GameObject p = Instantiate(
                    breakParticlesPrefab,
                    spawnPos,
                    breakParticlesPrefab.transform.rotation
                );
                // auto-destroy particles after they finish
                var ps = p.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(p, ps.main.duration + ps.main.startLifetime.constantMax);
                }
                else
                {
                    Destroy(p, 2f); // fallback
                }
            }

            // --- Instantly "break" block visually / physically ---
            GetComponent<Collider>().enabled = false;

            foreach (var r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            // --- Destroy object after sound is done ---
            if (breaking != null)
                Destroy(gameObject, breaking.length);
            else
                Destroy(gameObject);
        }
    }
}