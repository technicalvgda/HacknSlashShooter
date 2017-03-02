using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
	public float fireRate = 0.25f; // Seconds per projectile
	public float ProjectileSpeed = 5;
	public GameObject ProjectilePrefab;

	private float _nextFire = 0f;

	/// <summary>
	/// Shoots projectile.
	/// </summary>
	/// <param name="Angle">Angle.</param>
	public void ShootInput(float Angle)
	{
		if (CanShoot ()) 
		{
			Projectile.create(ProjectilePrefab, transform.gameObject, Angle, ProjectileSpeed);
			_nextFire = Time.time + fireRate;
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

}
