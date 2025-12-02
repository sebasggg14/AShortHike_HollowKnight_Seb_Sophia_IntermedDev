using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerStay (Collider collider)
    {
        if (collider.CompareTag("Stick") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Destroyed block");
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Stick") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Destroyed block");
            Destroy(gameObject);
        }

    }
}
