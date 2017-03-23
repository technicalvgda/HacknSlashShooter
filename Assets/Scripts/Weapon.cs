using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {	
	public float RPM = 120; //rounds per minute
	public float ProjectileSpeed = 5;
	public GameObject ProjectilePrefab;
	public int bulletsPerShot = 1; // Number of bullets per shot	
	public float bulletSpread = 0; // Wideness of shot in degrees
	public float baseRPM;

	private float _nextFire = 0f;
	private enum ProjectileType { ANTITANK, ANTIRANGE, NORMAL};
	void Start()
	{
		baseRPM = RPM;
	}
	/// <summary>
	/// Shoots projectile.
	/// </summary>
	/// <param name="Angle">Angle.</param>
	public void ShootInput(float Angle)
	{
		if (CanShoot ()) 
		{
			shoot (Angle);
			_nextFire = Time.time + 60/ RPM;
		}
	}

	/// <summary>
	/// Determines whether this instance can shoot.
	/// </summary>
	/// <returns><c>true</c> if this instance can shoot; otherwise, <c>false</c>.</returns>
	bool CanShoot()
	{
		return (Time.time >= _nextFire);
	}

	void shoot(float angle)
	{
		for (int bulletNum = 0; bulletNum < bulletsPerShot; bulletNum++) 
		{
			angleBullets (bulletSpread, bulletNum, bulletsPerShot, angle);
		}
	}

	/// <summary>
	/// Angles the bullets.
	/// </summary>
	/// <param name="bulletSpread">Bullet spread.</param>
	/// <param name="bulletNumber">Bullet number.</param>
	/// <param name="bulletsPerShot">Bullets per shot.</param>
	void angleBullets(float bulletSpread, int bulletNumber, int bulletsPerShot, float angle)
	{
		float bulletSpreadRads = bulletSpread * Mathf.Deg2Rad;	// Convert to rads
		float initialAngle =  bulletSpreadRads / 2;
		float bulletAngle = bulletNumber  *  bulletSpreadRads / (bulletsPerShot - 1);
		float offset =  bulletAngle - initialAngle;		// Calculates relative offset angle for projectile

		if (bulletsPerShot == 1) {
			Projectile.create (ProjectilePrefab, transform.gameObject, angle, ProjectileSpeed);
		} else {
			Projectile.create (ProjectilePrefab, transform.gameObject, angle + offset, ProjectileSpeed);
		}
	}
}
