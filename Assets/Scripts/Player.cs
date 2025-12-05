using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // for room transition
    public bool isActive = true;

    // for camera offset 
    public int lastInputHorizontal = 0; // -1 means left, 1 means right

    //change this to change movement speed of player:
    public float speed;
    public int maxFeathers;

    //current # of feathers
    public int feathers;
    private Rigidbody rb;

    //change this to change height of jumps:
    public float jumpForce;
    public float regenTimer = 0f;
    //change this to change cooldown time for feather regen
    public float featherRegenCooldown = 4.0f;
    //ref feather ui
    public FeatherUI featherPanel;

    //directional booleans for combat orientation
    bool left = true;
    bool right = true;
    bool up = true;
    bool down = true;
    public bool isGrounded = true;

    //health
    public int health = 5;

    //enum for attack states 
    enum PlayerStates
    {
        attacking,
        idling
    }

    public static bool canAttack = true;

    PlayerStates states = PlayerStates.idling;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feathers = maxFeathers;
        rb = GetComponent<Rigidbody>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("gravity" + rb.linearVelocity);
        float inputX = 0f;
        if (Input.GetKey(KeyCode.A) && left && isActive)
        {
            // left 
            inputX = -1f;
            lastInputHorizontal = -1;
            rb.MoveRotation(Quaternion.Euler(0, 323.934f, 0));
        }
        if (Input.GetKey(KeyCode.D) && right && isActive)
        {
            inputX = 1f;
            lastInputHorizontal = 1;
            rb.MoveRotation(Quaternion.Euler(0, 203.934f, 0));
        }

        Vector3 velocity = rb.linearVelocity;
        velocity.x = inputX * speed;
        rb.linearVelocity = velocity;

        // for smooth transition between scenes 
        if (!isActive)
        {
            if (lastInputHorizontal == -1)
            {
                velocity.x = lastInputHorizontal * speed;
                rb.linearVelocity = velocity;
            }

            if (lastInputHorizontal == 1)
            {
                velocity.x = lastInputHorizontal * speed;
                rb.linearVelocity = velocity;
            }
        }

        if (Input.GetMouseButtonDown(0) && isActive)
        {
            canAttack = false;
            states = PlayerStates.attacking;
        }
        else
        {
            states = PlayerStates.idling;
        }

        if (states == PlayerStates.attacking)
        {
            PlayerAttack();
        }

        //taking damage
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            if (isGrounded)
            {
                // FIRST JUMP (free)
                Vector3 v = rb.linearVelocity;
                v.x = 0; // Clear push-against-wall force
                rb.linearVelocity = v;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                isGrounded = false;
            }
            else if (feathers > 0)
            {
                // DOUBLE JUMP (costs 1 feather)
                feathers--;
                Vector3 v = rb.linearVelocity;
                v.x = 0; // Clear push-against-wall force
                rb.linearVelocity = v;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                //destroy feather icon
                GameObject lastObject = featherPanel.totalFeathers[featherPanel.totalFeathers.Count - 1];
                Destroy(lastObject);
                featherPanel.totalFeathers.RemoveAt(featherPanel.totalFeathers.Count - 1);
                //reset timer
                regenTimer = 0f;
            }
        }

        //feather regeneration after losing a feather:
        if (feathers < maxFeathers)
        {
            AddFeathers();
        }
    }

    IEnumerator AttackDuration()
    {
        //Debug.Log("attack started");
        yield return new WaitForSeconds(1f);
        //Debug.Log("attack finished");
        canAttack = true;
    }

    void AddFeathers()
    {
        if (feathers >= maxFeathers) return; //does not go past max

        regenTimer += Time.deltaTime;

        if (regenTimer >= featherRegenCooldown)
        {
            feathers++;
            featherPanel.AddFeatherUI();
            regenTimer = 0f; // restart timer for next feather
        }
        //Debug.Log("Num Feathers Available: " + feathers); 
    }

    void PlayerAttack()
    {
        //Debug.Log("attacked");
        StartCoroutine(AttackDuration());
        states = PlayerStates.idling;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true; //ground check for double jump
        }
    }

}
