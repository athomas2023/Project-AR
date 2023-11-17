using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI batteryText;

    private void Start()
    {
        // Check if the application is running on a mobile device (Android) or in the Unity Editor
        if (Application.isMobilePlatform)
        {
            StartCoroutine(UpdateBatteryStatus());
        }
        else
        {
            batteryText.text = "PC Power N/A";
        }
    }

    private IEnumerator UpdateBatteryStatus()
    {
        while (true)
        {
            // Read the battery status in percentage
            int batteryLevel = GetBatteryLevel();

            // Display the battery status
            batteryText.text = "Battery: " + batteryLevel + "%";

            yield return new WaitForSeconds(60.0f); // Update battery status every 60 seconds
        }
    }

    private int GetBatteryLevel()
    {
        if (Application.isMobilePlatform)
        {
            // Use the Android API to get the battery level
            try
            {
                AndroidJavaObject unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intentFilter = new AndroidJavaObject("android.content.IntentFilter", "android.intent.action.BATTERY_CHANGED");
                AndroidJavaObject batteryStatus = unityActivity.Call<AndroidJavaObject>("registerReceiver", null, intentFilter);

                int level = batteryStatus.Call<int>("getIntExtra", "level", -1);
                int scale = batteryStatus.Call<int>("getIntExtra", "scale", -1);

                if (level != -1 && scale != -1)
                {
                    return (int)((float)level / (float)scale * 100.0f);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error getting battery level: " + e.ToString());
            }
        }

        return -1;
    }
}
