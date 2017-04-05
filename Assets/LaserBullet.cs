﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour {
    public float maxSize = 5;
    public float time;
    private PlayerController player;
    private float angle;
    private bool paused = true;

    // Use this for initialization
    void Start () {
        time = Time.realtimeSinceStartup;
        player = FindObjectOfType<PlayerController>();
        angle = player.GetAngle(player.transform.position, player.GetMousePos());
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.localScale.x < maxSize)
        {
            float scaleUp = (Time.realtimeSinceStartup - time)/2;
            transform.localScale += new Vector3(scaleUp, scaleUp, scaleUp);
        }
        else
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
            }
            Vector3 pos = transform.position;
            pos.x += 5 * Mathf.Cos(angle) * Time.deltaTime;
            pos.z += 5 * Mathf.Sin(angle) * Time.deltaTime;
            transform.position = pos;
        }

        if(Time.realtimeSinceStartup - time > 7)
        {
            Destroy(this.gameObject);
        }
       
	}
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Enemy") || col.gameObject.name.Contains("Range"))
        {
            Destroy(col.gameObject);
        }
    }
}
