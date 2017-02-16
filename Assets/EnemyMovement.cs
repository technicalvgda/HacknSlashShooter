using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	UnityEngine.AI.NavMeshAgent nav;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}
		
	public void ChasePlayer()
	{
		nav.SetDestination (player.position); // Sets Enemy destination towards Player
	}

	// Use when Enemy or Player is Dead
	public void StopChasingPlayer()
	{
		nav.enabled = false;
	}
}
