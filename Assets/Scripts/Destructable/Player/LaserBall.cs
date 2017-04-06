using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBall : MonoBehaviour {
    public GameObject throwmarker;
    public GameObject decoy;
    private Vector3 _heightOfDecoy = new Vector3(0, 0.1f, 0);




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
        if (Input.GetButton("Fire2") && !GameObject.Find("Decoy(Clone)"))
        {
            //Temporary markery effect thing, fix/change later
            //possible put this into a another script that handles the ability itself(not projectionpowerup.cs)
            GameObject m = Instantiate(throwmarker, player.GetComponent<PlayerController>().GetMousePos() , transform.rotation);
            Destroy(m, 0.5f);
            GameObject d = Instantiate(decoy, transform.position, transform.rotation);
            d.GetComponent<ProjectionPowerup>().Activate(m.transform.position + _heightOfDecoy);
        }

    }

    void Fire()
    {
        Time.timeScale = 0;
        Instantiate(bullet, player.transform.position, player.transform.rotation);
    }
}
