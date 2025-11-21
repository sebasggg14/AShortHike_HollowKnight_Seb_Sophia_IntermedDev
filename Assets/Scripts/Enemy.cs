using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && Player.canAttack == false)
        {
            health--;
            //ground check for double jump
        }
    }


}
