﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBall : MonoBehaviour {
    public GameObject throwmarker;
    public GameObject decoy;
    private Vector3 _heightOfDecoy = new Vector3(0, 0.1f, 0);
    public GameObject bullet;
    public float throwRange = 7.5f;
    private GameObject player;
    private RaycastHit hit;
    private Vector3 direction;
    private Vector3 mousePos;
    private PlayerController pc;

    public currentpower powerup = currentpower.none;

    public enum currentpower
    {
        none,
        decoy,
        laser,
    }
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
        pc = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2") && powerup == currentpower.laser)
        {
            Fire();
        }
        if (Input.GetButton("Fire2") && !GameObject.Find("DEcoy(Clone)") && powerup == currentpower.decoy)
        {
            mousePos = PlayerController.GetMousePos();

            direction = mousePos - player.transform.position;
            if (Physics.Raycast(player.transform.position, direction, out hit, throwRange))
            {
                Debug.DrawLine(player.transform.position, hit.point);
                if (hit.collider.gameObject.tag == "Ground")
                {
                    GameObject m = Instantiate(throwmarker, mousePos, transform.rotation);
                    Destroy(m, 0.5f);
                    GameObject d = Instantiate(decoy, transform.position, transform.rotation);
                    d.GetComponent<ProjectionPowerup>().Activate(m.transform.position + _heightOfDecoy);
                }
            }
        }

    }

    void Fire()
    {
        Instantiate(bullet, player.transform.position, player.transform.rotation);
    }
}
