using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnswersHolder : MonoBehaviour
{
    [SerializeField]
    private List<Questions> questions = new List<Questions>();

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private string FileName = "Results.txt";

    [SerializeField]
    private string FileLocation = "";

    private int currentQuestion;

    private List<Answers> selectedAnswers = new List<Answers>();

    [Serializable]
    public class Questions
    {
        public Answers[] answer;
    }

    [Serializable]
    public class Answers
    {
        public Button answerButton;
        public Text answerText;
        public bool correct;
    }

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(SubmitAnswers);

        for (int i = 0; i < questions.Count - 1; i++)
        {
            for (int j = 0; j < questions[i].answer.Length; i++)
            {
                questions[i].answer[j].answerButton.onClick.AddListener(() => CollectAnswers(questions[i].answer[j]));
            }
        }
    }

    private void CollectAnswers(Answers selectedAnswer)
    {
        if (selectedAnswers.Contains(selectedAnswer))
        {
            selectedAnswers.RemoveAt(currentQuestion);
        }

        selectedAnswers.Insert(currentQuestion, selectedAnswer);
    }

    private void SubmitAnswers()
    {
        string path = Path.Combine(FileLocation, FileName);

        string[] data = selectedAnswers.Select(answer => answer.ToString()).ToArray();

        File.WriteAllLines(path, data);
    }
}
