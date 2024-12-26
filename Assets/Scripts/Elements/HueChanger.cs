using UnityEngine;

public class HueChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float hueChangeSpeed = 1.0f; // Speed of hue change
    
    private float hue, saturation, value;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize HSV values from the current color
        Color.RGBToHSV(spriteRenderer.color, out hue, out saturation, out value);
    }

    void Update()
    {
        // Increment the hue value over time
        hue += hueChangeSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f; // Wrap hue value between 0 and 1

        // Update the sprite's color with the new hue
        spriteRenderer.color = Color.HSVToRGB(hue, saturation, value);
    }
}
