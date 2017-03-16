using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class EnemyGun : MonoBehaviour {
	public float projectileWait = 0.03f;//1 second intervals
	public float ProjectileSpeed = 5.0f;//shoots with force of 1 
	public GameObject ProjectilePrefab;
	public int bulletsPerShot = 1; // Number of bullets per shot	
	public float bulletSpread = 0; // Wideness of shot in degrees

	private bool canfire;
	private GameObject player;

	// Use this for initialization
	void Start () {
		canfire = true; //initializes shooting boolean to not shooting
		player = GameObject.FindGameObjectWithTag("Player"); //finds the player game object
	}


	float GetAngle(Vector3 v1, Vector3 v2)
	{
		return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
	}

	public void startShooting()//function call to start coroutine
	{
		if (canfire)//if enemy is already shooting do nothing
		{
			canfire = false;
			Timing.RunCoroutine(EnemyShoot()); //start shooting

		}
	}
		
	IEnumerator<float> EnemyShoot()
	{
		spreadShot ();
		yield return Timing.WaitForSeconds(projectileWait);// wait projectile wait time before firing again
		canfire = true; //set shooting boolean to true
	}
		
	void spreadShot()
	{
		for (int bulletNum = 0; bulletNum < bulletsPerShot; bulletNum++) 
		{
			angleBullets (bulletSpread, bulletNum, bulletsPerShot);
		}
	}

	/// <summary>
	/// Angles the bullets.
	/// </summary>
	/// <param name="bulletSpread">Bullet spread.</param>
	/// <param name="bulletNumber">Bullet number.</param>
	/// <param name="bulletsPerShot">Bullets per shot.</param>
	void angleBullets(float bulletSpread, int bulletNumber, int bulletsPerShot)
	{
		float bulletSpreadRads = bulletSpread * Mathf.Deg2Rad;	// Convert to rads
		float initialAngle =  bulletSpreadRads / 2;
		float bulletAngle = bulletNumber  *  bulletSpreadRads / (bulletsPerShot - 1);
		float offset =  bulletAngle - initialAngle;  // Calculates relative offset angle for projectile

		if (bulletsPerShot == 1) {
			Projectile.create (ProjectilePrefab, transform.gameObject, GetAngle (this.transform.position, player.transform.position), ProjectileSpeed);
		} else {
			Projectile.create (ProjectilePrefab, transform.gameObject, GetAngle (this.transform.position, player.transform.position) + offset, ProjectileSpeed);
		}
	}

	public void setBulletSpread(float bulSprd)
	{
		bulletSpread = bulSprd;
	}

	public void setBulletsPerShot(int bps)
	{
		bulletsPerShot = bps;
	}
}
