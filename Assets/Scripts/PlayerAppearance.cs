using UnityEngine;
using System.Collections;

public class PlayerAppearance : MonoBehaviour
{
    
    // for character blinking 
    [SerializeField]
    Texture openEyesRed;
    [SerializeField]
    Texture closeEyesRed;
    [SerializeField]
    Texture openEyesBlue;
    [SerializeField]
    Texture closeEyesBlue;
    
    [SerializeField]
    GameObject child;

    Material childMaterial;
    Renderer childRenderer;

    public bool isJumping = false;
    bool blinking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childRenderer = child.GetComponent<Renderer>();
        childMaterial = childRenderer.material;
        StartCoroutine(BlinkLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesBlue;
        }
        else if (!isJumping && !blinking)
        {
            childMaterial.mainTexture = openEyesRed;
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

            // done blinking; Update() will restore the correct open-eye texture
            blinking = false;
        }
    }
}