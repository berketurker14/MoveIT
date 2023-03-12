using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject spritePrefab;
    public float width = 30;
    public float height = 10;
    public float spacing = 1;

    void Start()
    {
        Vector2 offset = new Vector2((width - 1) * spacing / 2f, (height - 1) * spacing / 2f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(x * spacing + 0.5f, y * spacing + 0.5f) - offset;
                Instantiate(spritePrefab, transform.position + (Vector3)position, Quaternion.identity, transform);
            }
        }
    }
}