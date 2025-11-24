using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [SerializeField]
    float parallaxStrength = 0f; // adjust from 0 (moves w/ cam) to 1 (doesn't move)

    [SerializeField]
    GameObject camera;

    private float startPos;
    float distance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
    }

    // fixedUpdate to prevent jitter
    void FixedUpdate()
    {
        distance = camera.transform.position.x * parallaxStrength;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
