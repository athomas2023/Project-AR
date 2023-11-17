using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class UserData
{
    public string fullName;
    public string homeAddress;
    public string emailAddress;
    public string phoneNumber;
}

public class DataManager : MonoBehaviour
{
    public TMP_InputField fullNameInput;
    public TMP_InputField homeAddressInput;
    public TMP_InputField emailAddressInput;
    public TMP_InputField phoneNumberInput;
    public Button continueButton;
    public Button startButton;
  

    private UserData userData;
    private string folderPath; // Path for the user data folder

    private void Start()
    {
        folderPath = GetUserDataFolderPath();
        EnsureUserDataFolderExists();

        continueButton.onClick.AddListener(OnContinueClick);
        startButton.onClick.AddListener(OnStartClick);
        
    }

    private void OnContinueClick()
    {
        userData = new UserData
        {
            fullName = fullNameInput.text,
            homeAddress = homeAddressInput.text,
            emailAddress = emailAddressInput.text,
            phoneNumber = phoneNumberInput.text
        };
    }

    private void OnStartClick()
    {
        if (userData != null)
        {
            SaveUserData(userData);
        }
    }

    private void OnDeleteUserDataClick()
    {
        DeleteUserDataFolder();
    }

    private void OnShowFolderClick()
    {
        ShowUserDataFolder();
    }

    private void SaveUserData(UserData data)
    {
        if (!string.IsNullOrEmpty(data.fullName))
        {
            string fileName = data.fullName.Split(' ')[0]; // Use the first name as the file name
            string filePath = Path.Combine(folderPath, fileName + ".txt");

            string userDataText = $"Full Name: {data.fullName}\n" +
                $"Home Address: {data.homeAddress}\n" +
                $"Email Address: {data.emailAddress}\n" +
                $"Phone Number: {data.phoneNumber}";

            File.WriteAllText(filePath, userDataText);
        }
    }

    private void EnsureUserDataFolderExists()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    private void DeleteUserDataFolder()
    {
        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }
    }

    private void ShowUserDataFolder()
    {
        Debug.Log("User Data Folder Location: " + folderPath);
    }

    private string GetUserDataFolderPath()
    {
#if UNITY_EDITOR
        // When running in the Unity Editor, save to a folder named "UserData" in the project's root directory
        return Path.Combine(Application.dataPath, "../UserData");
#else
        // On Android, save to a folder named "UserData" in the persistent data directory
        return Path.Combine(Application.persistentDataPath, "UserData");
#endif
    }
}
