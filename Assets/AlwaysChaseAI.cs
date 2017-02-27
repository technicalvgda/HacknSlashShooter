using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysChaseAI : MonoBehaviour {

    private EnemyMovement _movement;

    // Use this for initialization
    void Start()
    {
        _movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(_vision.alertness == 0)
        {
            //do nothing for now
        }
        if(_vision.alertness >= 1)
        {
            transform.LookAt(_vision.personalLastSighting);
			_movement.ChasePlayer ();
            _weapon.startShooting();
        }*/
        _movement.ChasePlayer();
    }
}


