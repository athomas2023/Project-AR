using UnityEngine;
using UnityEditor;
using System.IO;

public class JSONImporterEditor : EditorWindow
{
    private string sourceFilePath = "";

    [MenuItem("Tools/Import JSON to Resources")]
    public static void ShowWindow()
    {
        GetWindow(typeof(JSONImporterEditor), false, "Import JSON to Resources");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import JSON File to Resources Folder", EditorStyles.boldLabel);

        if (GUILayout.Button("Select JSON File"))
        {
            sourceFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        }

        GUILayout.Label("Selected File: " + (string.IsNullOrEmpty(sourceFilePath) ? "None" : Path.GetFileName(sourceFilePath)));

        if (GUILayout.Button("Import JSON"))
        {
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                EditorUtility.DisplayDialog("Error", "No JSON file selected!", "OK");
                return;
            }

            string resourcesFolderPath = Path.Combine(Application.dataPath, "Resources");

            // Create Resources folder if it doesn't exist
            if (!Directory.Exists(resourcesFolderPath))
            {
                Directory.CreateDirectory(resourcesFolderPath);
                AssetDatabase.Refresh();
            }

            // Copy the JSON file to the Resources folder
            string destinationFileName = Path.GetFileName(sourceFilePath);
            string destinationFilePath = Path.Combine(resourcesFolderPath, destinationFileName);

            File.Copy(sourceFilePath, destinationFilePath, true);
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Success", $"Imported {destinationFileName} to Resources folder.", "OK");
        }
    }
}
