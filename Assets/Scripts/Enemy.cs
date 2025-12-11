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
        if (switchDir)
        {
            rb.MoveRotation(Quaternion.Euler(0, 297.14f, 0));
            walkRight();
        }
        if (!switchDir)
        {
            rb.MoveRotation(Quaternion.Euler(0, 66.269f, 0));
            walkLeft();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HealthDuration()
    {
        //Debug.Log("attack started");
        yield return new WaitForSeconds(2f);
        //Debug.Log("attack finished");
        canTakeDamage = true;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Stick") && Player.canAttack == false && !canTakeDamage)
        {
            health--; //enemy takes damage
        }
        //player takes damage
        if (collider.CompareTag("Player") && canTakeDamage)
        {
            player.health --;
            //destroy health icon
            GameObject lastObject = healthPanel.totalHealth[healthPanel.totalHealth.Count - 1];
            Destroy(lastObject);
            healthPanel.totalHealth.RemoveAt(healthPanel.totalHealth.Count - 1);
            canTakeDamage = false;
            StartCoroutine(HealthDuration());
        }

    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Stick") && Player.canAttack == false && !canTakeDamage)
        {
            health--; //enemy takes damage
        }
        //player takes damage
        if (collider.CompareTag("Player") && canTakeDamage)
        {
            player.health --;
            //destroy health icon
            GameObject lastObject = healthPanel.totalHealth[healthPanel.totalHealth.Count - 1];
            Destroy(lastObject);
            healthPanel.totalHealth.RemoveAt(healthPanel.totalHealth.Count - 1);
            canTakeDamage = false;
            StartCoroutine(HealthDuration());
        }

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
