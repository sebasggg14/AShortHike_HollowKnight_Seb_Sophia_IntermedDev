using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    List<Transform> instantiatedCreatures = new List<Transform>();

    // collider for the camera
    BoxCollider currentRoom;

    //[SerializeField]
    //Transform topLeftBorder;
    //[SerializeField]
    //Transform bottomRightBorder;
    
    // adjust this for camera
    [SerializeField]
    float smoothValue = 1.0f;

    Vector3 startPos;
    Vector3 velocity = Vector3.zero;

    public Player movement; // reference to whatever script has the movement code

    [SerializeField]
    float cameraXOffset = 0f;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CameraRoom"))
        {
            currentRoom = col.GetComponent<BoxCollider>();
        }
    }

    void Update()
    {
        instantiatedCreatures.Clear(); // so it updates constantly

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj  in player)
        {
            instantiatedCreatures.Add(obj.transform); // add the transform of the game object 
        }

        // ---------------------------------
        // okay I just realized that in HK the camera doesnt average out the pos of other creatures in the game
        // comented it out but could potentially use it later (+ it's not working 100% anyways)
        // -------------------------
        //foreach(GameObject obj in enemy)
        //{
        //    if (obj.transform.position.x > topLeftBorder.position.x && obj.transform.position.x < bottomRightBorder.position.x &&
        //        obj.transform.position.y > topLeftBorder.position.y && obj.transform.position.y < bottomRightBorder.position.y)
        //    {
        //        instantiatedCreatures.Add(obj.transform); // add the transform ONLY IF its within the camera borders
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        Vector3 targetPos = Vector3.zero; // this is what the camera will track to

        if (instantiatedCreatures.Count == 1)
        {
            targetPos = instantiatedCreatures[0].transform.position;
            if (movement.lastInputHorizontal == -1) // if the last key pressed was a (making the char face the left)...
            {
                targetPos.x = targetPos.x - cameraXOffset;
            }
            else if (movement.lastInputHorizontal == 1)
            {
                targetPos.x = targetPos.x + cameraXOffset;
            }
            else
            {
                return;
            }
        }
        // ---------------------------------
        // okay I just realized that in HK the camera doesnt average out the pos of other creatures in the game
        // comented it out but could potentially use it later (+ it's not working 100% anyways)
        // -------------------------
        //else // if there are other creatures in the cameras view... 
        //{
        //    int numOfEnemiesInScreen = 0;
        //    foreach (Transform creature in instantiatedCreatures)
        //    {
        //        targetPos += creature.position;
        //        numOfEnemiesInScreen++;
        //    }

        //    targetPos /= numOfEnemiesInScreen; // average out the targets pos by all enemies in scene
        //}

        if (currentRoom != null)
        {
            Bounds b = currentRoom.bounds;

            targetPos.x = Mathf.Clamp (targetPos.x, b.min.x, b.max.x);
            targetPos.y = Mathf.Clamp (targetPos.y, b.min.y, b.max.y);
        }

        // check if its working
        if (currentRoom == null)
        {
            Debug.Log("No camera room set");
        }
        else
        {
            Debug.Log("Clamping to room " + currentRoom.name);
        }

        // make camera lerp to target
        targetPos.z = -16f;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothValue);
    }
}
