using UnityEngine;
using TMPro;

public class FlickeringTextController : MonoBehaviour
{
    public TextMeshProUGUI dynamicTextTMP; // Reference to the TextMeshProUGUI UI element
    public float flickerSpeed = 10.0f;
    public float flickerIntensity = 0.5f;
    public Color baseColor = Color.white;
    public Color flickerColor = Color.yellow;

    private void Start()
    {
        if (dynamicTextTMP != null)
        {
            dynamicTextTMP.color = baseColor;
        }
    }

    private void Update()
    {
        if (dynamicTextTMP != null)
        {
            float flicker = Mathf.Sin(Time.time * flickerSpeed) * flickerIntensity;
            dynamicTextTMP.color = Color.Lerp(baseColor, flickerColor, flicker);
        }
    }

    public void UpdateText(string newMessage)
    {
        if (dynamicTextTMP != null)
        {
            dynamicTextTMP.text = newMessage; // Update text content
        }
    }
}
