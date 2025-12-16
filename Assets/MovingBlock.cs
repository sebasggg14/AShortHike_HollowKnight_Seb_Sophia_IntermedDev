using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public bool switchDir = true;
    public float time = 0;
    public int maxTime = 3;
    public float speed;

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
            walkRight();
        }
        if (!switchDir)
        {
            walkLeft();
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
