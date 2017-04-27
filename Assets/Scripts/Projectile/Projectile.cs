using System.Collections;
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


		//////////////////////////////////////////////////////////////////////////////////////// 
		/// This determines whether the projectile has a specific active augment component if the player's
		/// weapon component is active and attached
		ComboBoostAugmentation cbAug = owner.GetComponentInChildren<ComboBoostAugmentation> ();
		SurgicalPrecisionAugmentation spAug = owner.GetComponentInChildren<SurgicalPrecisionAugmentation> ();
		if ( spAug != null) 
		{
			// IF the augmentation is Enabled
			if (spAug.isActiveAndEnabled) 
			{
				// IF this projectile doesn't have this bullet augmentation
				if (obj.GetComponent<SurgicalPrecisionBulletAugmentation> () == null) 
				{
					// Add that component to this projectile
					obj.AddComponent<SurgicalPrecisionBulletAugmentation> ();
				}
				// IF this projectile does have this bullet augmentation
				else 
				{	// Make this true
					obj.GetComponent<SurgicalPrecisionBulletAugmentation> ().enabled = true;
				}
					
			}
			// IF the augmentation is Disabled
			else 
			{
				// IF this projectile has the augmentation
				if (obj.GetComponent<SurgicalPrecisionBulletAugmentation> () != null) 
				{
					// Disables the augmentation
					obj.GetComponent<SurgicalPrecisionBulletAugmentation> ().enabled = false;
				}
			}	

		}

		if ( cbAug != null) 
		{
			if (cbAug.isActiveAndEnabled) 
			{
				if (obj.GetComponent<ComboBoostBullet> () == null) 
				{
					obj.AddComponent<ComboBoostBullet> ();
				}
				else 
				{
					obj.GetComponent<ComboBoostBullet> ().enabled = true;
				}
			} 
			else 
			{
				if (obj.GetComponent<ComboBoostBullet> () != null) 
				{
					obj.GetComponent<ComboBoostBullet> ().enabled = false;
				}
			}	
				
		}
		/////////////////////////////////////////////////////////////////////////////////////////

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
			if (GetComponent<SurgicalPrecisionBulletAugmentation>() != null && GetComponent<SurgicalPrecisionBulletAugmentation>().isActiveAndEnabled) 
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
