using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public Canvas canvas;

	// Use this for initialization
	void Start () {
        GetComponent<Canvas>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
