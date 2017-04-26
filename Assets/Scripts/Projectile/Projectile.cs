﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject owner;
    public float angle;
    public float Speed;

	public float maxTravelTime = 3; // The life time of a projectile before it deactivates in seconds
	private float _currTravelTime = 0;


	public static GameObject create(GameObject parent, GameObject owner, float Angle, float speed, float maxTravelTime)
	{

		Vector3 pos = owner.transform.position;
		pos.x += 1f * Mathf.Cos(Angle);
		pos.z += 1f * Mathf.Sin(Angle);
		pos.y += .25f;
		GameObject obj = PoolManager.Create(parent, pos, owner.transform.rotation);
		Projectile proj = obj.GetComponent<Projectile>();
		proj = proj == null ? obj.AddComponent<Projectile>() : proj;
		proj.owner = owner;
		proj.angle = Angle;
		proj.Speed = speed;
		proj._currTravelTime = 0;
		proj.maxTravelTime = maxTravelTime;

		if (owner.GetComponentInChildren<ComboBoostAugmentation> () != null) 
		{
			if (obj.GetComponent<ComboBoostBullet> () == null) {
				obj.AddComponent<ComboBoostBullet> ();
			}
		}
		if (owner.GetComponentInChildren<SurgicalPrecisionAugmentation> () != null) 
		{
			if (obj.GetComponent<SurgicalPrecisionBulletAugmentation> () == null) {
				obj.AddComponent<SurgicalPrecisionBulletAugmentation> ();
			}
		}
		return obj;
	}

	public static GameObject create(GameObject parent, GameObject owner, float Angle, float speed)
	{
		return create(parent, owner, Angle, speed, 3);
	}
        


    void Update()
    {
		// Destroy projectile after maxTravelTime seconds
		_currTravelTime += Time.deltaTime;
		if (_currTravelTime >= maxTravelTime) 
		{
			// Logic for the Surgical Precision Augmentation
			if (GetComponent<SurgicalPrecisionBulletAugmentation>() != null) 
			{
				GetComponent<SurgicalPrecisionBulletAugmentation> ().signalBulletDespawned ();
			}

			PoolManager.Destroy (transform.gameObject);
		}

        Vector3 pos = transform.position;
        pos.x += Speed * Mathf.Cos(angle) * Time.deltaTime;
        pos.z += Speed * Mathf.Sin(angle) * Time.deltaTime;
        transform.position = pos;

    }

}
