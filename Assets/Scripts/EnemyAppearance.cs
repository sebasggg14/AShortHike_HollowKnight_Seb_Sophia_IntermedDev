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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying && enemy.health <= 0)
        {
            isDying = true;

            animator.ResetTrigger("Attack");     // stop attack from interrupting
            animator.SetTrigger("Death");        // prefer trigger for one-shot death
            return;
        }

        if (isDying) return; // nothing else can interrupt death

        if (enemy.isAttacking)
        {
            animator.SetTrigger("Attack");
            enemy.isAttacking = false; // IMPORTANT: clear it here or via animation event
        }
        Debug.Log($"health={enemy.health}, enemyRef={enemy.name}", this);
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
