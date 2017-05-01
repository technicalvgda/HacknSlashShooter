using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    //public float duration = 120.0f;
    public bool finished = false;
    private bool canCount = false;
    private float timer;
    private Text TimerUI;
    private Spawner spawner;
    void Start() {
        TimerUI = GetComponent<Text>();
    }

	void Update () {
        if (canCount)
        {
            timer -= Time.deltaTime;
            SetTimerDisplay();
            if(timer <= 0.0f)
            {
                TimerUI.text = null;
                canCount = false;
                finished = true;
                spawner.SetAggro(2);
            }
            
        }
	}

    private void SetTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60.0f);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerUI.text = formattedTime;
    }

    public void StartTimer(float d, Spawner s)
    {
        spawner = s;
        spawner.SetAggro(1);
        timer = d;
        canCount = true;
    }

    public void StopTimer()
    {
        TimerUI.text = null;
        canCount = false;
        finished = true;
    }
}
