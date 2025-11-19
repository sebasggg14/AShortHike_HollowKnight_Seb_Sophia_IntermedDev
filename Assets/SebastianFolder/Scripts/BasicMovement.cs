using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float speed = 10f;

    public int lastInputHorizontal = 0; // 0 means left, 1 means right 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position; // fetches the position from the transform component. Vector means coordinates & direction in x, y, & z.
        if (Input.GetKey(KeyCode.W))
        {
            currentPos.y += speed * Time.deltaTime; // if button pressed, add 0.5 to the squares position. manupilates speed by tying the movement to the framerate
        }

        if (Input.GetKey(KeyCode.S))
        {
            currentPos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            currentPos.x -= speed * Time.deltaTime;
            lastInputHorizontal = -1; // left 
        }

        if (Input.GetKey(KeyCode.D))
        {
            currentPos.x += speed * Time.deltaTime;
            lastInputHorizontal = 1; // right
        }

        transform.position = currentPos; // sets the transformation to the vector3 


    }
}
