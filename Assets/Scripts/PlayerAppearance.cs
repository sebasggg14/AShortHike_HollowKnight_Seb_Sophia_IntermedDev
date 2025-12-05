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
        StartCoroutine(BlinkLoop());
        StartCoroutine(IdleLoop());
    }

    // Update is called once per frame
    void Update()
    {
        // blinking --------------------------
        if (isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesBlue;
        }
        else if (!isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesRed;
        }

        // jumping -----------------------
        if (Input.GetKeyDown(KeyCode.Space) && !player.isGrounded)
        {
            animator.SetTrigger("InitialJump");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            animator.SetTrigger("FeatherJump");
        }
        
        if (rigidbody.linearVelocity.y > 0 || rigidbody.linearVelocity.y == 0)
        {
            animator.SetBool("isFalling", false);
        }
        else
        {
            animator.SetBool("isFalling", true);
        } 
        

        // walking -----------------
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // attacking --------------------------
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

    }

    public void IdleAnimationFinished()
    {
        isIdling = false;
    }

    IEnumerator IdleLoop()
    {
        while (true)
        {

            if (!isIdling)
            {
                yield return new WaitForSeconds(Random.Range(2.5f, 5f));
                isIdling = true;
                animator.SetTrigger("Idle");
            }
            else
            {
                yield return null; // so the coroutine closes and doesnt crash unity
            }
        }
    }

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

            // how long the eyes stay closed
            yield return new WaitForSeconds(0.25f);

            // done blinking
            blinking = false;
        }
    }
}