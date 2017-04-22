using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour {
    public Animator anim;

    private float inputH;
    private float inputV;
    private float angle;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 lookDir = PlayerController.GetMousePos();
        
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        float lookDifference = PlayerController.GetAngle(new Vector3(inputH, 0, inputV), lookDir);
        Debug.Log(Mathf.Abs(lookDifference));
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
    }
}
