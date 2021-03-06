﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MovementEffects;

public class pauseMenu : MonoBehaviour {
    private AudioSource buttonClick;
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name.Contains("Arena") || SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("Animation"))
        {
            this.gameObject.SetActive(false);
        }
        buttonClick = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Resume()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void Exit()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("MainMenu");
        }

    public void StartGame()
    {
        Timing.KillAllCoroutines();
        PoolManager.ResetPoolManager();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("MainLevel");
    }

    public void Settings()
    {

    }

    public void Arena()
    {
        Timing.KillAllCoroutines();
        PoolManager.ResetPoolManager();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("ArenaPrep");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
