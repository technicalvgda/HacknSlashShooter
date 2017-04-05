﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBall : MonoBehaviour {
    public GameObject bullet;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Fire();
        }
        
	}

    void Fire()
    {
        Time.timeScale = 0;
        Instantiate(bullet, player.transform.position, player.transform.rotation);
    }
}
