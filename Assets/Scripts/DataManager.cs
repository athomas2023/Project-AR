using UnityEngine;
using TMPro;
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
    public TextMeshProUGUI NameOverwrite;

    private UserData userData;
    private string folderPath; // Path for the user data folder

    private void Start()
    {
        folderPath = GetUserDataFolderPath();
        EnsureUserDataFolderExists();
    }

    public void OnContinueClick()
    {
        userData = new UserData
        {
            fullName = fullNameInput.text,
            homeAddress = homeAddressInput.text,
            emailAddress = emailAddressInput.text,
            phoneNumber = phoneNumberInput.text
        };

         OnStartClick();
         NameOverwrite.text = fullNameInput.text;
    }

    public void OnStartClick()
    {
        if (userData != null)
        {
            SaveUserData(userData);
        }
    }

    public void OnDeleteUserDataClick()
    {
        DeleteUserDataFolder();
    }

    public void OnShowFolderClick()
    {
        ShowUserDataFolder();
    }

    private void SaveUserData(UserData data)
    {
        if (!string.IsNullOrEmpty(data.fullName))
        {
            string fileName = data.fullName.Split(' ')[0]; // Use the first name as the file name
            string filePath = Path.Combine(folderPath, fileName + ".json");

            string userDataJson = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, userDataJson);
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
        // When running in the Unity Editor, save to the desktop
        return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "UserData");
#else
        // On Android, save to a folder named "UserData" in the persistent data directory
        return Path.Combine(Application.persistentDataPath, "UserData");
#endif
    }
}
