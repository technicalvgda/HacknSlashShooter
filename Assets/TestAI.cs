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
	private EnemyMovement _movement;
    public string[] priorityTargetTag;
    private GameObject _primary, _secondary, _tertiary, _current;
	// Use this for initialization
	void Start () {
        _vision = GetComponent<EnemyVision>();
        _weapon = GetComponent<EnemyGun>();
		_movement = GetComponent<EnemyMovement> ();
	}
	
    //sets the priority target to the one specified
    private void SetPriority(string target) {
        _primary = GameObject.FindGameObjectWithTag(target);
    }

	// Update is called once per frame
	void Update () {
        //find some better way to get constant updates on who is on the field
        //or a way to notify all enemies a new entity is on the field?
        _primary = GameObject.FindGameObjectWithTag(priorityTargetTag[0]);
        _secondary = GameObject.FindGameObjectWithTag(priorityTargetTag[1]);
        _tertiary = GameObject.FindGameObjectWithTag(priorityTargetTag[2]);

        if (_vision.CanSeeTarget(_primary) >= 1) {
            _current = _primary;
        }else if(_vision.CanSeeTarget(_secondary) >= 1) {
            _current = _secondary;
        }else if(_vision.CanSeeTarget(_tertiary) >= 1) {
            _current = _tertiary;
        }else {
            _current = null;
        }

        if (_current != null) {
            transform.LookAt(_current.transform.position);
            _movement.ChaseTarget(_current);
            if (_vision.CanSeeTarget(_current) >= 2) {
                if (_weapon != null)
                {
                    _weapon.startShooting();
                }
            }
        }
        //check the priority target first
        //if the priotity target is in range, skip the player check
            //else check for the playe rinstead
            //calling _vision.canseetarget and getting back the 1 return
		
	}
}
