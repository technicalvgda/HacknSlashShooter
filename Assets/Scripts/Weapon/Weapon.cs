﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    //sound
    public AudioClip shootSound;

    private AudioSource source;
    //endsound
    public float RPM = 120; //rounds per minute
	public float ProjectileSpeed = 5;
	public float projectileTravelTime = 1; // Lifetime of projectile in seconds
	public GameObject ProjectilePrefab;
	public int bulletsPerShot = 1; // Number of bullets per shot	
	public float bulletSpread = 0; // Wideness of shot in degrees
	public float baseRPM;

	private float rpmMultiplier = 1;

	KnockBackAugmentation _kbAug;

	private float _nextFire = 0f;
	private enum ProjectileType { ANTITANK, ANTIRANGE, NORMAL};
	void Start()
	{
		baseRPM = RPM;
        //sound
        source = GetComponent<AudioSource>();
        //endsound

		_kbAug = GetComponent<KnockBackAugmentation> ();
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
			_nextFire = Time.time + 60/ (RPM * rpmMultiplier);
		}
	}

	/// <summary>
	/// Determines whether this instance can shoot.
	/// </summary>
	/// <returns><c>true</c> if this instance can shoot; otherwise, <c>false</c>.</returns>
	public bool CanShoot()
	{
		return (Time.time >= _nextFire);
	}

	void shoot(float angle)
	{
        //sound
        source.PlayOneShot(shootSound);
        //endsound

		// Checks for knock back augment
		if (_kbAug != null) {
			_kbAug.augmentShot ();
		}

		for (int bulletNum = 0; bulletNum < bulletsPerShot; bulletNum++) 
		{
			angleBullets (bulletSpread, bulletNum, bulletsPerShot, angle);
		}
	}



	/// <summary>
	/// Angles the bullets.
	/// </summary>
	/// <param name="bulletSpread">Bullet spread in radians.</param>
	/// <param name="bulletNumber">Bullet number.</param>
	/// <param name="bulletsPerShot">Bullets per shot.</param>
	void angleBullets(float bulletSpread, int bulletNumber, int bulletsPerShot, float angle)
	{
		float bulletSpreadRads = bulletSpread * Mathf.Deg2Rad;	// Convert to rads
		float initialAngle =  bulletSpreadRads / 2;
		float bulletAngle = bulletNumber  *  bulletSpreadRads / (bulletsPerShot - 1);
		float offset =  bulletAngle - initialAngle;		// Calculates relative offset angle for projectile

		if (bulletsPerShot == 1) {
			Projectile.create (ProjectilePrefab, transform.parent.gameObject, angle, ProjectileSpeed, projectileTravelTime);
		} else {
			Projectile.create (ProjectilePrefab, transform.parent.gameObject, angle + offset, ProjectileSpeed, projectileTravelTime);
		}
	}

	public void boostRPM(float multiplier)
	{
		rpmMultiplier =  multiplier;
	}

	public void resetRPM()
	{
		rpmMultiplier = 1;
	}

}
