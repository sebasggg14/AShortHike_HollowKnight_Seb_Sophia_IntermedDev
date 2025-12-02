using UnityEngine;
using System.Collections.Generic;
public class HealthUI : MonoBehaviour
{
    public RectTransform parent;
    public GameObject imagePrefab;
    public Player player;
    public int spacing = 50;

    public List<GameObject> totalHealth = new List<GameObject>();

    void Start()
    {
        // Create starting icons for maxFeathers
        for (int i = 0; i < player.health; i++)
        {
            CreateHealthIcon(i);
        }
    }

    void CreateHealthIcon(int index) //left here just in case we want to implement heal
    {
        GameObject newImageObject = Instantiate(imagePrefab, parent, false);

        RectTransform rt = newImageObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(index * spacing, 0);

        totalHealth.Add(newImageObject);
    }

    // Called when a feather regenerates
    public void AddHealthUI()
    {
        // only add if we have fewer icons than feathers
        if (totalHealth.Count >= player.health)
            return;

        int index = totalHealth.Count;   // next slot on the right
        CreateHealthIcon(index);
    }
}
