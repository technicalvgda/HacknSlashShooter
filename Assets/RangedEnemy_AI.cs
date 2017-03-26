using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

/**
*   The Ranged Unit 
*   Teleports around the player every XX seconds
*   Teleport range is a hollow circle, inside firign distance, but outside a certain range
*   
*   Attacks are single fire towards the player
*
*   Behavior
*   Teleports towards the player (minimum distance) to fire,then teleports freely within range once more
* 
**/

public class RangedEnemy_AI : MonoBehaviour {
    private EnemyVision _vision;
    private EnemyGun _weapon;
    private RangedMovement _movement;
    private bool _canMove = true, _canAttack = true;
    public float attackDelay, moveDelay = 5.0f;

    // Use this for initialization
    void Start()
    {
        _vision = GetComponent<EnemyVision>();
        _weapon = GetComponent<EnemyGun>();
        _movement = GetComponent<RangedMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_vision.alertness == 0)
        {
            //do nothing for now
        }
        if (_vision.alertness >= 1)
        {
            if (_canAttack)
            {
                Timing.RunCoroutine(AttackDelay());
                _weapon.startShooting();
            }
            else if (_canMove)
            {
                Timing.RunCoroutine(MoveDelay());
            }
        }
        transform.LookAt(_vision.personalLastSighting);
    }

    private IEnumerator<float> AttackDelay()
    {
        _canAttack = false;
        _movement.AttackMove();
        yield return Timing.WaitForSeconds(attackDelay);
        _canAttack = true;

    }

    private IEnumerator<float> MoveDelay()
    {
        _canMove = false;
        _movement.NormalMove();
        yield return Timing.WaitForSeconds(moveDelay);
        _canMove = true;
    }

}
