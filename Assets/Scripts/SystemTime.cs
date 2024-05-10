using System;
using UnityEngine;
using TMPro;

public class SystemTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        // You might want to update the time every frame
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        // Get the current system time
        DateTime currentTime = DateTime.Now;

        // Format the time as a string
         string formattedTime = currentTime.ToString("h:mm:ss tt");

        // Update the TextMeshPro text
        timeText.text = formattedTime;
    }
}
