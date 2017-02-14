using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestTrigger : MonoBehaviour {
    public GameObject testPoint;
    private CameraController _cControl;
	// Use this for initialization
	void Start () {
       _cControl = Camera.main.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            _cControl.MoveCamera(testPoint.transform, 1.0f);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            _cControl.RelockCamera();
        }
    }
}
