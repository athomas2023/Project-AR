using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public List<Button> answerButtons;
    public TextMeshProUGUI scoreText;

    [Header("Scores")]
    public int score = 0;
    public float percentage = 0f;

    [Header("Quiz Data")]
    public QuizData quizData; // Reference to the ScriptableObject

    private List<QuizData.Question> questions;
    private int currentQuestionIndex;

    private void Start()
    {
        if (quizData == null)
        {
            Debug.LogError("QuizData ScriptableObject is not assigned to the QuizManager.");
            return;
        }

        LoadQuestionsFromScriptableObject();
        RandomizeQuestions();
        currentQuestionIndex = 0;
        score = 0;
        UpdateScoreText();
        DisplayQuestion();
    }

    private void LoadQuestionsFromScriptableObject()
    {
        // Assuming quizData has an array or list of questions
        questions = quizData.questions.ToList();
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
            QuizData.Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;

            for (int i = 0; i < answerButtons.Count; i++)
            {
                TextMeshProUGUI optionText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i < currentQuestion.options.Length)
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
        QuizData.Question currentQuestion = questions[currentQuestionIndex];
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
