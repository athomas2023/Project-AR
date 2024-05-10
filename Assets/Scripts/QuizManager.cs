using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public List<string> options;
        public string answer;
    }

    [System.Serializable]
    public class Quiz
    {
        public string quiz;
        public List<Question> questions;
    }

    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public List<Button> answerButtons;
    public TextMeshProUGUI scoreText;

    // Make these public so that they are visible in the Inspector
    [Header("Scores")]
    public int score = 0;
    public float percentage = 0f;

    private List<Question> questions;
    private int currentQuestionIndex;

    private void Start()
    {
        LoadQuestions();
        RandomizeQuestions();
        currentQuestionIndex = 0;
        score = 0;
        UpdateScoreText();
        DisplayQuestion();
    }

    private void LoadQuestions()
    {
        string path = Path.Combine(Application.dataPath, "Resources/questions.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Quiz quiz = JsonUtility.FromJson<Quiz>(json);
            questions = quiz.questions;
        }
        else
        {
            Debug.LogError("Questions JSON file not found at path: " + path);
        }
    }

    private void RandomizeQuestions()
    {
        System.Random rnd = new System.Random();
        questions = questions.OrderBy(x => rnd.Next()).ToList();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;

            for (int i = 0; i < answerButtons.Count; i++)
            {
                TextMeshProUGUI optionText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i < currentQuestion.options.Count)
                {
                    optionText.text = currentQuestion.options[i];
                    answerButtons[i].gameObject.SetActive(true);
                    int index = i;
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(optionText.text));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            ShowFinalResults();
        }
    }

    private void CheckAnswer(string selectedOption)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (selectedOption == currentQuestion.answer)
        {
            score++;
        }

        currentQuestionIndex++;
        UpdateScoreText();
        DisplayQuestion();
    }

    private void UpdateScoreText()
    {
        percentage = ((float)score / questions.Count) * 100;
        scoreText.text = $"Score: {score}/{questions.Count}\nGrade: {percentage}%";
    }

    private void ShowFinalResults()
    {
        UpdateScoreText();
        questionText.text = $"Quiz Completed!\nCorrect Answers: {score}/{questions.Count}\nGrade: {percentage}%";
        scoreText.gameObject.SetActive(true);

        // Hide all answer buttons
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
