using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameControllerClass : MonoBehaviour
{

    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text editEndText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionindex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private void Start()
    {

        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;

        playerScore = 0;
        questionindex = 0;

        ShowQuestion();
        isRoundActive = true;

    }


    //pass question for displaying
    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionindex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);

        }

    }


    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }

    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        if (questionPool.Length > questionindex + 1)
        {
            questionindex++;
            ShowQuestion(); 
        }
        else
        {
            EndRound();
        }


    }

    //turn off question, turn on round end
    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);

        string end1 = "Congratulation you finished first round with: ";
        string end2 = " points!";
        editEndText.text = end1 + playerScore + end2;
        roundEndDisplay.SetActive(true);
    }

  

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

