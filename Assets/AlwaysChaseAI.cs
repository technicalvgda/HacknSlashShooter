using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysChaseAI : MonoBehaviour {
    public string[] priorityTargetTag = new string[3];
    private GameObject _primary, _secondary, _tertiary, _current;
    private EnemyMovement _movement;

    // Use this for initialization
    void Start()
    {
        _movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        _primary = GameObject.FindGameObjectWithTag(priorityTargetTag[0]);
        _secondary = GameObject.FindGameObjectWithTag(priorityTargetTag[1]);
        _tertiary = GameObject.FindGameObjectWithTag(priorityTargetTag[2]);
       
        if(_current == null)
        {
            _current = GameObject.FindGameObjectWithTag("Player");
        }
        if(_primary != null)
        {
            _current = _primary;
        }
        else if(_secondary != null)
        {
            _current = _secondary;
        }
        else if(_tertiary != null)
        {
            _current = _tertiary;
        }
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
        _movement.ChaseTarget(_current);
    }
}


