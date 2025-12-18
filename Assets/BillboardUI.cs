using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        var cam = Camera.main;
        if (!cam) return;

        transform.forward = cam.transform.forward; // faces camera without flipping
    }
}
