using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    [SerializeField] // animator ref
    Animator animator;

    [SerializeField] // player script ref 
    Enemy enemy;

    [SerializeField]
    GameObject enemyObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isAttacking && !animator.GetBool("Death"))
        {
            animator.SetTrigger("Attack");
        }

        if (enemy.health <= 0)
        {
            animator.SetBool("Death", true);
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
