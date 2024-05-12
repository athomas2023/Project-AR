using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/Create New Quiz Data")]
public class QuizData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] options;
        public string answer;
    }

    public Question[] questions;
}
