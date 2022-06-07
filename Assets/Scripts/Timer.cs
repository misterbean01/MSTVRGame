using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerTxt;
    private TimeSpan timePlaying;
    private float elapsedTime;
    public bool isTimerOn = true;

    // Start is called before the first frame update
    void Start()
    {
        timerTxt.text = "00:00.00";
        elapsedTime = 0f;
        StartCoroutine("UpdateElapsedTime");
    }

    private IEnumerator UpdateElapsedTime() {

        while (isTimerOn) {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timerStr = timePlaying.ToString("mm':'ss'.'ff");
            timerTxt.text = timerStr;

            yield return null;
        }
    }
}
