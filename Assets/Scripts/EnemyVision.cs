using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
    public float viewDist = 5.0f; //distance from enemy that the enemy will see the player 
    public bool sight = false;//bool for if the enemy can see player 

    private RaycastHit _hit;//hit from raycast
    private Vector3 _playerLocation, _raycastDirection, _startVec;
    private GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");//find player game object
	}
	
	// Update is called once per frame
	void Update () {
        _startVec = this.transform.position;//enemy position
        _playerLocation = _player.transform.position;//player current position

        _raycastDirection = _playerLocation - _startVec;//raycast from the enemy towards the player
        //Debug.Log("something");
        if(Vector3.Distance(_playerLocation, _startVec) < viewDist) //if the player is within vision range
        {
            //Debug.Log("player within range");
            if (Physics.Raycast(_startVec, _raycastDirection, out _hit, viewDist) && _hit.collider.gameObject.transform.parent.tag == "Player")//send raycast and if it hits the player object
            {
               transform.LookAt(_player.transform.position); // rotate enemy to look at the player
               sight = true;
                GetComponent<EnemyGun>().startShooting();//start shooting at player
               //Debug.Log("I see the player"); 
            }
            else
            {
                sight = false;
            }    
        }
        else
        {
            sight = false;
        }
	}
}


