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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = parent.GetComponent<Rigidbody>();
        
        childRenderer = child.GetComponent<Renderer>();
        childMaterial = childRenderer.material;
        StartCoroutine(BlinkLoop()); // this still repeats because of the while loop in the coroutine
        StartCoroutine(IdleLoop()); // same case as BlinkLoop
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        
        if (rigidbody.linearVelocity.y > 0.01 || rigidbody.linearVelocity.y == 0) // 0.01 offset to accomodate float point accuracy 
        {
            animator.SetBool("isFalling", false);
        }
        else if (rigidbody.linearVelocity.y < -0.01 ) // 0.01 offset to accomodate float point accuracy 
        {
            animator.SetBool("isFalling", true);
        } 

        if (player.isGrounded)
        {
            isJumping = false;
        }
        

        // walking -----------------
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
            CanceledIdle();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // attacking --------------------------
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            animator.SetTrigger("Attack");
        }

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