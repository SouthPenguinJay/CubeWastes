using UnityEngine;
using TMPro; // Namespace for TextMesh Pro
using System.Collections.Generic;

public class DynamicTextTMPController : MonoBehaviour
{
    public TextMeshProUGUI dynamicTextTMP; // Reference to the TextMeshProUGUI UI element
    public List<MessageDuration> messagesWithDurations; // List of messages with their durations

    private void Start()
    {
        if (dynamicTextTMP != null && messagesWithDurations.Count > 0)
        {
            UpdateTextRandomly(); // Set initial text randomly
        }
    }

    private void UpdateTextRandomly()
    {
        if (dynamicTextTMP != null && messagesWithDurations.Count > 0)
        {
            int randomIndex = Random.Range(0, messagesWithDurations.Count); // Select a random message index
            MessageDuration selectedMessage = messagesWithDurations[randomIndex];
            dynamicTextTMP.text = selectedMessage.message; // Update text content
            Invoke("UpdateTextRandomly", selectedMessage.duration); // Schedule next update
        }
    }

    [System.Serializable]
    public class MessageDuration
    {
        public string message;
        public float duration;
    }
}
