using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health;
    public float time = 0;
    public int maxTime = 3;
    public bool switchDir = true;
    public float speed;

    public Player player;

    public bool canTakeDamage = true;

    public HealthUI healthPanel;

    [SerializeField]
    Rigidbody rb;

    public bool isAttacking = false;

    bool dying = false;

    bool canEnemyTakeDamage = true;
    bool canPlayerTakeDamage = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            switchDir = true;
        }
        if (time >= maxTime)
        {
            switchDir = false;
        }
        if (switchDir && !dying)
        {
            rb.MoveRotation(Quaternion.Euler(0, 297.14f, 0));
            walkRight();
        }
        if (!switchDir && !dying)
        {
            rb.MoveRotation(Quaternion.Euler(0, 66.269f, 0));
            walkLeft();
        }

        if (health <= 0)
        {
            maxTime = 0;
            time = 0;
            ObjectivesTracker.enemiesKilled++;
            dying = true;
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        // PLAYER → ENEMY (stick attack)
        if (other.CompareTag("Stick") && !Player.canAttack && canEnemyTakeDamage)
        {
            TakeEnemyDamage();
        }

        // ENEMY → PLAYER (body contact)
        if (other.CompareTag("Player") && canPlayerTakeDamage && health > 0)
        {
            TakePlayerDamage();
        }
    }

    void TakeEnemyDamage()
    {
        health--;
        canEnemyTakeDamage = false;
        StartCoroutine(EnemyDamageCooldown());
    }

    void TakePlayerDamage()
    {
        player.health--;

        GameObject lastObject =
            healthPanel.totalHealth[healthPanel.totalHealth.Count - 1];
        Destroy(lastObject);
        healthPanel.totalHealth.RemoveAt(healthPanel.totalHealth.Count - 1);

        canPlayerTakeDamage = false;
        StartCoroutine(PlayerDamageCooldown());
    }

    IEnumerator EnemyDamageCooldown()
    {
        yield return new WaitForSeconds(0.4f);
        canEnemyTakeDamage = true;
    }

    IEnumerator PlayerDamageCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        canPlayerTakeDamage = true;
    }

    void walkLeft() {
        Vector3 currentPos = transform.position;
        currentPos.x -= speed * Time.deltaTime;
        transform.position = currentPos;
        time -= Time.deltaTime;
    }

    void walkRight() {
        Vector3 currentPos = transform.position;
        currentPos.x += speed * Time.deltaTime;
        transform.position = currentPos;
        time += Time.deltaTime;
    }


}
