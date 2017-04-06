﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {

    //sound
    public AudioClip bulletImpactWall;
    public AudioClip bulletImpactEnemy;
    private AudioSource source;
    //end sound

	public float damage = 5.0f;					// base damage of bullet
	public float critMultiplier = 2.0f;			// crit multiplier
	public float resistanceMultiplier = 0.5f;	// Damage reduction multiplier, should be < 1
	public bool canPierceThroughEnemies = true;

	public enum ProjectileType { ANTITANK, ANTIRANGE, ANTISWARM, NORMAL }
	public ProjectileType projectileType;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

	void OnTriggerEnter(Collider col)
	{
		DestructableData hit;
		GameObject owner = GetComponent<Projectile> ().owner; 
		if (hit = col.GetComponent<DestructableData> ())
		{
			//When the owner is destroyed
			if (owner != null && owner.name == "Player") 
			{
				// Check if player didn't hit themself
				if (col.GetComponent<PlayerController> () == null) 
				{
					damageEnemy (hit, col);
                    source.PlayOneShot(bulletImpactEnemy);
					if (!canPierceThroughEnemies) 
					{
						PoolManager.Destroy (transform.gameObject);
					}
				}
			//Assume that the Enemy fired the projectile
			} 
			else 
			{
				if (col.GetComponent<PlayerController>() != null) 
				{
					hit.TakeDamage (damage);
					PoolManager.Destroy(transform.gameObject);
				}
			}

		}
		//NOTE: Probably find a different way to check what can't be passed through by the projectile if the condition gets too long
		else if(col.GetComponent<Spawner>() == null && col.GetComponent<Projectile>() == null && col.GetComponent<SlidingTwoDoor>() == null)
		{
            source.PlayOneShot(bulletImpactWall);
			PoolManager.Destroy(transform.gameObject);
		}
	}

	/// <summary>
	/// Damages the enemy.
	/// </summary>
	/// <param name="hit">Reference to enemy's Destructable Data</param>
	/// <param name="col">Reference to enemy's </param>
	void damageEnemy(DestructableData hit, Collider col){
		switch (projectileType) {
		case ProjectileType.ANTIRANGE:
			if (col.GetComponent<EnemyGun> () != null) 
			{
				hit.TakeDamage (damage * critMultiplier);
			} else 
			{
				hit.TakeDamage (damage * resistanceMultiplier);
			}
			break;
		case ProjectileType.ANTITANK:
			//Placeholder for tank enemies
			hit.TakeDamage(damage);
			/*
			if(col.GetComponent<TankComponent>())
			{
				hit.TakeDamage(damage*critMultiplier);
			} else 
			{
				hit.TakeDamage (damage*resistanceMultiplier);
			}
			*/
			break;
		case ProjectileType.ANTISWARM:
			if (col.GetComponent<SlowPlayer> () != null) 
			{
				hit.TakeDamage (damage * critMultiplier);
			} else 
			{
				hit.TakeDamage (damage * resistanceMultiplier);
			}
			break;
		default:
			break;
		}
	}
}