using System.Collections;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class RegenSpeed : MonoBehaviour {

    private float MaxSpeed;
    private PlayerController Speed;
    private bool isSlowed;
    private bool isRunning;
    public float regenDelay;
    public float regenTimer = 0;

	// Use this for initialization
	void Start () {
        Speed = GetComponent<PlayerController>();
        isSlowed = false;
        isRunning = false;
        regenDelay = 5;

    }
	
	// Update is called once per frame
	void Update () {
        
        if (Speed.Speed < MaxSpeed && !isRunning)
        {
            Debug.Log("slowed");
            isSlowed = true;
            Timing.RunCoroutine(_regenSpeed());
        }
    }

    IEnumerator<float> _regenSpeed()
    {
        isRunning = true;
        while (isSlowed)
        {
            while(regenTimer < regenDelay)
            {
                yield return Timing.WaitForSeconds(1.0f);
                regenTimer++;
            }
            if(Speed.Speed < MaxSpeed)
            {
                yield return Timing.WaitForSeconds(2.0f);

                Speed.Speed++;
            }
            isSlowed = false;
        }
        isRunning = false;
    }
}
