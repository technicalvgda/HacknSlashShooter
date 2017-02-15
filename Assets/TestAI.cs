using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FOR TEST PURPOSES ONLY
/// 
/// Simple AI that takes what the vision detects 
/// </summary>
public class TestAI : MonoBehaviour {
    private EnemyVision _vision;
    private EnemyGun _weapon;
	// Use this for initialization
	void Start () {
        _vision = GetComponent<EnemyVision>();
        _weapon = GetComponent<EnemyGun>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_vision.alertness == 0)
        {
            //do nothing for now
        }
        if(_vision.alertness >= 1)
        {
            transform.LookAt(_vision.personalLastSighting);
            _weapon.startShooting();
        }
	}
}
