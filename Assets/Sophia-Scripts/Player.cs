using UnityEngine;

public class Player : MonoBehaviour
{
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feathers = maxFeathers;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement:
        Vector3 currentPos = transform.position;
        if (Input.GetKey(KeyCode.A) && left)
        {
            currentPos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && right)
        {
            currentPos.x += speed * Time.deltaTime;
        }
        transform.position = currentPos;

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
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

    void AddFeathers()
    {
        if (feathers >= maxFeathers) return; //does not go past max

        regenTimer += Time.deltaTime;

        if (regenTimer >= featherRegenCooldown)
        {
            feathers++;
            regenTimer = 0f; // restart timer for next feather
        }
        Debug.Log("Num Feathers Available: " + feathers); 
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true; //ground check for double jump
        }
    }

}
