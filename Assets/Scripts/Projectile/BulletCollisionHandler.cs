using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour {
	public float damage = 5.0f;					// base damage of bullet
	public float critMultiplier = 2.0f;			// crit multiplier
	public float resistanceMultiplier = 0.5f;	// Damage reduction multiplier < 1
	public bool canPierce = true;

	public ProjectileType projectileType = ProjectileType.ANTIRANGE;
	void OnTriggerEnter(Collider col)
	{
		DestructableData hit;
		/* TODO: Consider if enemies are the ones shooting
		 * 			Enemies should not damage other enemies
		 * 			They should only damage the player/friendlies
		 * 		Player bullets should not harm themselves although player may never reach the bullet
		*/ 
		if (hit = col.GetComponent<DestructableData> ())
		{
			switch (projectileType) {
			case ProjectileType.ANTIRANGE:
				if (col.CompareTag ("Range")) {
					hit.TakeDamage (damage * critMultiplier);
				} else {
					hit.TakeDamage (damage*resistanceMultiplier);
				}
				break;
			case ProjectileType.ANTITANK:
				if(col.CompareTag("Tank")){
					hit.TakeDamage(damage*critMultiplier);
				} else {
					hit.TakeDamage (damage*resistanceMultiplier);
				}
				break;
			case ProjectileType.ANTISWARM:
				if(col.CompareTag("Swarmer")){
					hit.TakeDamage(damage*critMultiplier);
				} else {
					hit.TakeDamage (damage*resistanceMultiplier);
				}
				break;
			case ProjectileType.NORMAL:
				if (col.CompareTag ("Player")) {
					hit.TakeDamage (damage);
				}
				break;
			default:
				hit.TakeDamage (damage*resistanceMultiplier);
				break;
			}
			if (!canPierce) {
				PoolManager.Destroy(transform.gameObject);
			}
		}
		else if(col.transform.name.Contains("Spawner"))
		{
		}
		else
		{
			PoolManager.Destroy(transform.gameObject);
		}
	}
}
