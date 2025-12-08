using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay (Collider collider)
    {
        if (collider.CompareTag("Stick") && !Player.canAttack)
        {
            Debug.Log("Destroyed block");
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Stick") && !Player.canAttack)
        {
            Debug.Log("Destroyed block");
            Destroy(gameObject);
        }

    }
}
