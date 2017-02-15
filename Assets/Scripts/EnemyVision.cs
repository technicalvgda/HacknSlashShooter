using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
    public float viewDist = 5.0f;
    public bool sight = false;

    

    private RaycastHit _hit;
    private Vector3 _playerLocation, _raycastDirection, _startVec;
    private GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        _startVec = this.transform.position;
        _playerLocation = _player.transform.position;

        _raycastDirection = _playerLocation - _startVec;
        //Debug.Log("something");
        if(Vector3.Distance(_playerLocation, _startVec) < viewDist)
        {
            //Debug.Log("player within range");
            if (Physics.Raycast(_startVec, _raycastDirection, out _hit, viewDist) && _hit.collider.gameObject.transform.parent.tag == "Player")
            {
               transform.LookAt(_player.transform.position);
               sight = true;
                GetComponent<EnemyGun>().startShooting();
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


