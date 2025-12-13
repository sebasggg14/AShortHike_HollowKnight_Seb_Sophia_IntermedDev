using UnityEngine;
using System.Collections;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField] // animator ref
    Animator animator;

    Rigidbody rigidbody; // rigibody of the player parent 

    [SerializeField]
    GameObject parent;

    [SerializeField] // player script ref 
    Player player;
    
    // ref for all textures for character blinking 
    [SerializeField]
    Texture openEyesRed;
    [SerializeField]
    Texture closeEyesRed;
    [SerializeField]
    Texture openEyesBlue;
    [SerializeField]
    Texture closeEyesBlue;
    
    [SerializeField] // ref to the object that has the material
    GameObject child;

    Material childMaterial; 
    Renderer childRenderer;

    public bool isJumping = false;
    public bool blinking = false;

    bool isIdling = false;

    AudioSource aud;
    public AudioClip attacking;

    private bool jumpTriggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = parent.GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
        childRenderer = child.GetComponent<Renderer>();
        childMaterial = childRenderer.material;
        StartCoroutine(BlinkLoop()); // this still repeats because of the while loop in the coroutine
        StartCoroutine(IdleLoop()); // same case as BlinkLoop
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGrounded", player.isGrounded);
        // jumping color switch --------------------------
        if (isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesBlue;
        }
        else if (!isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesRed;
        }

        // jumping action -----------------------
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            animator.SetTrigger("InitialJump");
            isJumping = true;
            CanceledIdle();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !player.isGrounded)
        {
            animator.SetTrigger("FeatherJump");
            isJumping = true;
        }

        // Reset jumpTriggered once we actually leave the ground
        if (jumpTriggered && !player.isGrounded)
            jumpTriggered = false;

        // End jump state when grounded again
        if (player.isGrounded)
            isJumping = false;


        // walking -----------------
        bool moveInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        animator.SetBool("isWalking", player.isGrounded && moveInput);

        // attacking --------------------------
        if (Input.GetMouseButtonDown(0) && Player.canAttack && !isJumping)
        {
            
            animator.SetTrigger("Attack");
            aud.PlayOneShot(attacking);
        }


    }

    private float lastVy;
    void FixedUpdate()
    {
        float vy = rigidbody.velocity.y;

        bool startedFalling = (lastVy > 0.05f && vy <= 0.05f); // crossed apex OR got stalled
        if (startedFalling) animator.SetBool("isFalling", true);

        // If actually moving upward again (double jump), clear falling
        if (vy > 0.05f) animator.SetBool("isFalling", false);

        // If grounded, clear falling
        if (player.isGrounded) animator.SetBool("isFalling", false);

        lastVy = vy;
    }

    // idle -----------------------------------------
    public void IdleAnimationFinished()
    {
        isIdling = false;
    }
    void CanceledIdle() // not public since im not using it in an anim
    {
        isIdling = false;
    }

    IEnumerator IdleLoop()
    {
        while (true)
        {

            if (!isIdling)
            {
                yield return new WaitForSeconds(Random.Range(2.5f, 10f));
                isIdling = true;
                animator.SetTrigger("Idle");
            }
            else
            {
                yield return null; // so the coroutine closes and doesnt crash unity
            }
        }
    }

    // blinking --------------------------------
    IEnumerator BlinkLoop()
    {
        while (true)
        {
            // wait a random amount of time before the next blink
            float waitTime = Random.Range(1f, 10f);
            yield return new WaitForSeconds(waitTime);

            // start blink
            blinking = true;

            // set closed eyes based on jump state
            if (isJumping)
                childMaterial.mainTexture = closeEyesBlue;
            else
                childMaterial.mainTexture = closeEyesRed;

            yield return new WaitForSeconds(0.25f);
            blinking = false; // finish
        }
    }
}