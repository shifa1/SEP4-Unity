using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public int timeLeft = 60;
    public Text countdownText;

    private void Start()
    {
        StartCoroutine("LoseTime");
    }

    private void Update()
    {
        countdownText.text = ("" + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = ("Times up!");
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
            yield return new WaitForSeconds(1);
        timeLeft--;
    }
}


