using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    private float cycleTimer = -1f;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        cycleTimer += Time.deltaTime / 50f;
        float t = (Mathf.Sin(cycleTimer) + 1f) / 2f; // sin returns [-1, 1], this will return [0, 1]
        Color newColor = image.color;
        float newAlpha = Mathf.Lerp(0f, 0.5f, t); // 0 = day, 0.5 = night
        newColor.a = newAlpha;
        image.color = newColor;
    }
}
