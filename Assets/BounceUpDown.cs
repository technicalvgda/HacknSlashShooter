using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MovementEffects;
using System;

public class BounceUpDown : MonoBehaviour {
    private bool active = false;
    private float peak;
    private float bottom;
    private float speed = 0.5f;
    private float startTime, distance = 0.25f;

	// Use this for initialization
	void Start () {
        peak = transform.position.y + 0.25f;
        bottom = transform.position.y;
	}
	
    void Update()
    {
        if (active)
        {
            if (transform.position.y == bottom)
            {
                transform.DOMoveY(peak, 0.5f, false);
            }
            else if (transform.position.y == peak)
            {
                transform.DOMoveY(bottom, 0.5f, false);
            }
        }
    }

    public void SetOn()
    {
        active = true;
    }

    public void SetOff()
    {
        active = false;
    }


}
