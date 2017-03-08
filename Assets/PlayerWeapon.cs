using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
	public float RPM = 120; //rounds per minute
	public float ProjectileSpeed = 5;
	public GameObject ProjectilePrefab;
    public float baseRPM;
	private float _nextFire = 0f;

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
			Projectile.create(ProjectilePrefab, transform.gameObject, Angle, ProjectileSpeed);
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

}
