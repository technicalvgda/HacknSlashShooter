using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Pause : MonoBehaviour {

    public GameObject pauseMenu;
    public Animator pauseButtons;

	void Update ()
    {
        if (pauseMenu != null && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseButtons.SetTrigger("Enter");
        }
	}
}
