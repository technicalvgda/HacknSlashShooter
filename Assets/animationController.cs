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
        
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        if (inputH != 0 || inputV != 0)
        {
            //float lookDifference = PlayerController.GetAngle(new Vector3(inputH, 0, inputV).normalized, transform.forward.normalized) / Mathf.PI * 180;

            
            //get the angle of the direction the player is looking
            float lookAngle = Mathf.Atan2(transform.forward.normalized.z, transform.forward.normalized.x) / Mathf.PI * 180;
            if (lookAngle < 0)//change the angle so it is in units of 360 degrees
            {
                lookAngle = 360 + lookAngle;
            }

            //get the angle of the direction the player is moving 
            Vector3 movementVector = new Vector3(inputH, 0, inputV).normalized;
            float moveAngle = Mathf.Atan2(movementVector.z, movementVector.x) / Mathf.PI * 180;
            if(moveAngle < 0)//change the angle so it is in units of 360 degrees
            {
                moveAngle = 360 + moveAngle;
            }

            //get the difference between the direction you are looking and moving 
            float lookDifference = moveAngle - lookAngle;
            if(lookDifference > 180)//make sure the values are between 180 and -180
            {
                lookDifference = lookDifference - 360;
            }
            if(lookDifference < -180)
            {
                lookDifference = lookDifference + 360;
            }
            //Debug.Log(lookDifference);
            

           anim.SetFloat("lookDif", lookDifference);//change value in the animator
            
        }
        else
        {
            anim.SetFloat("lookDif", 0); // if the player is not moving then set the animator value to 0
            
        }
        
    }
}
