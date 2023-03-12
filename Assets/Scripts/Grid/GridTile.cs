using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Color lightGreen;
    public Color lighterGreen;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = RandomColor();
    }

    Color RandomColor()
    {
        float r = Random.Range(lightGreen.r, lighterGreen.r);
        float g = Random.Range(lightGreen.g, lighterGreen.g);
        float b = Random.Range(lightGreen.b, lighterGreen.b);
        return new Color(r, g, b);
    }
}
