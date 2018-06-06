using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private const float TIME_BEFORE_START = 3.0f;
    public GameObject pauseMenu;

    public Text timerText;

    public int timeLeft;

    private float startTime = 0;



    private void Start()
    {
        pauseMenu.SetActive(false);
        startTime = Time.time;

        StartCoroutine("LoseTime");
    }

    private void Update()
    {
        timerText.text = ("" + timeLeft);

        if (Time.time - startTime < TIME_BEFORE_START)
            return;

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            timerText.text = ("Time is up!");


            SceneManager.LoadScene("Persistent");
        }

        
    }

    //toggle pause
    public void TogglePauseMenu()
    {
       
        //changing scale 0-1
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 0 : 1;

    }

    

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //return to menu
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            if (timeLeft > 60)
            {
                timerText.text = ("60");
                yield return new WaitForSeconds(5);
                timeLeft = timeLeft - 5;
            }
            else
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
            
        
        }
    }

}
