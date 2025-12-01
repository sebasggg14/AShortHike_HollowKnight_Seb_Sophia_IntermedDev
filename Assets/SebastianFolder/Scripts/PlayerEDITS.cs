using UnityEngine;
using System.Collections;

public class PlayerEDITS : MonoBehaviour
{
    // for room transition
    public bool isActive = true;
    
    // for camera offset 
    public int lastInputHorizontal = 0; // -1 means left, 1 means right

    //change this to change movement speed of player:
    public float speed;
    public int maxFeathers;

    //current # of feathers
    int feathers;
    private Rigidbody rb;

    //change this to change height of jumps:
    public float jumpForce;
    private float regenTimer = 0f;
    //change this to change cooldown time for feather regen
    public float featherRegenCooldown = 4.0f;

    //directional booleans for combat orientation
    bool left = true;
    bool right = true;
    bool up = true;
    bool down = true;
    bool isGrounded = true;

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
        Debug.Log(canAttack);
        Debug.Log("is active? " + isActive);
        //movement:
        Vector3 currentPos = transform.position;
        currentPos.z = 0f;
        float yRotation = transform.localEulerAngles.y;
        if (yRotation > 180f) yRotation -= 360f;

        if (Input.GetKey(KeyCode.A) && left && isActive)
        {
            currentPos.x -= speed * Time.deltaTime;
            lastInputHorizontal = -1; // left 
            if (yRotation <= -44)
            {
                transform.Rotate(0, 270 * Time.deltaTime, 0);
            }
        }
        if (Input.GetKey(KeyCode.D) && right && isActive)
        {
            currentPos.x += speed * Time.deltaTime;
            lastInputHorizontal = 1; // right
            if (yRotation >= -136.7f)
            {
                transform.Rotate(0, -270 * Time.deltaTime, 0);
            }
        }
        transform.position = currentPos;

        // for smooth transition between scenes 
        if (!isActive)
        {
            if (lastInputHorizontal == -1)
            {
                currentPos.x -= speed * Time.deltaTime;
            }

            if (lastInputHorizontal == 1)
            {
                currentPos.x += speed * Time.deltaTime;
            }
        }
        transform.position = currentPos;

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

        if (states == PlayerStates.idling)
        {
            //canAttack = true;
        }

        

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            if (isGrounded)
            {
                // FIRST JUMP (free)
                rb.AddForce(Vector3.up * jumpForce);
                isGrounded = false;
            }
            else if (feathers > 0)
            {
                // DOUBLE JUMP (costs 1 feather)
                feathers--;
                rb.AddForce(Vector3.up * jumpForce);
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
        Debug.Log("attack started");
        yield return new WaitForSeconds(1f);
        Debug.Log("attack finished");
        canAttack = true;
    }

    //IEnumerator AttackCooldown()
    //{
        
    //}

    void AddFeathers()
    {
        if (feathers >= maxFeathers) return; //does not go past max

        regenTimer += Time.deltaTime;

        if (regenTimer >= featherRegenCooldown)
        {
            feathers++;
            regenTimer = 0f; // restart timer for next feather
        }
        //Debug.Log("Num Feathers Available: " + feathers); 
    }

    void PlayerAttack()
    {
        Debug.Log("attacked");
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
