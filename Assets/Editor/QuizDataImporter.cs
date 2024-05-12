using UnityEditor;
using UnityEngine;
using System.IO;

public class QuizDataImporter : Editor
{
    [MenuItem("Quiz/Import Quiz Data From JSON")]
    public static void ImportQuizData()
    {
        string path = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string jsonData = File.ReadAllText(path);
        QuizData quizData = CreateInstance<QuizData>();
        JsonUtility.FromJsonOverwrite(jsonData, quizData);

        AssetDatabase.CreateAsset(quizData, "Assets/QuizData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = quizData;
    }
}
