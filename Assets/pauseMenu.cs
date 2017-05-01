using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MovementEffects;

public class pauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name.Contains("Arena") || SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("Animation"))
        {
            this.gameObject.SetActive(false);
        }
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
        
        SceneManager.LoadScene("Main Menu");
        

    }

    public void StartGame()
    {
        Timing.KillAllCoroutines();
        PoolManager.ResetPoolManager();
        SceneManager.LoadScene("MainLevel");
    }

    public void Settings()
    {

    }

    public void Arena()
    {
        Timing.KillAllCoroutines();
        PoolManager.ResetPoolManager();
        SceneManager.LoadScene("Arena");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
