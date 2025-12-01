using UnityEngine;
using System.Collections.Generic;

public class FeatherUI : MonoBehaviour
{
    public RectTransform parent;
    public GameObject imagePrefab;
    public Player player;
    public int spacing = 50;

    public List<GameObject> totalFeathers = new List<GameObject>();

    void Start()
    {
        // Create starting icons for maxFeathers
        for (int i = 0; i < player.maxFeathers; i++)
        {
            CreateFeatherIcon(i);
        }
    }

    void CreateFeatherIcon(int index)
    {
        GameObject newImageObject = Instantiate(imagePrefab, parent, false);

        RectTransform rt = newImageObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(index * spacing, 0);

        totalFeathers.Add(newImageObject);
    }

    // Called when a feather regenerates
    public void AddFeatherUI()
    {
        // only add if we have fewer icons than feathers
        if (totalFeathers.Count >= player.feathers)
            return;

        int index = totalFeathers.Count;   // next slot on the right
        CreateFeatherIcon(index);
    }
}
