using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    [SerializeField] // animator ref
    Animator animator;

    [SerializeField] // player script ref 
    Enemy enemy;

    [SerializeField]
    GameObject enemyObject;

    private bool isDying;

    AudioSource aud;

    [SerializeField]
    AudioClip deathSFX;

    [Header("Effects")]
    public GameObject breakParticlesPrefab; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aud = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying && enemy.health <= 0)
        {
            isDying = true;

            animator.ResetTrigger("Attack");     // stop attack from interrupting
            aud.PlayOneShot(deathSFX);
             // --- Spawn particles ---
            if (breakParticlesPrefab != null)
            {
            Vector3 spawnOffset = new Vector3(0f, 3f, 1f);
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
            animator.SetTrigger("Death");        // prefer trigger for one-shot death
            return;
        }

        if (isDying) return; // nothing else can interrupt death

        if (enemy.isAttacking)
        {
            animator.SetTrigger("Attack");
            enemy.isAttacking = false; // IMPORTANT: clear it here or via animation event
        }
    }

    public void updateIsAttacking()
    {
        enemy.isAttacking = false;
    }

    public void destroyGameObject()
    {
        Destroy(enemyObject);
    }
}
