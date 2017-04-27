using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Surgical precision bullet augmentation.
/// 
/// Handles the collision logic for the Surgical precision augmentation.
/// </summary>
public class SurgicalPrecisionBulletAugmentation : MonoBehaviour {

	private SurgicalPrecisionAugmentation _surgPrecAug;

	void Start() {
		_surgPrecAug = GetComponent<Projectile> ().owner.GetComponentInChildren<SurgicalPrecisionAugmentation> ();
	}

	void OnTriggerEnter (Collider col)
	{
		if (this.enabled) 
		{
			//Debug.Log ("Collided with " + col.name);
			// Everytime you hit an enemy, increase the precision counter
			if (col.CompareTag ("Enemy")) 
			{
				_surgPrecAug.incrementPrecisionCounter ();
			} 
			else 
			{
				// List of colliders that should not reset the precision counter
				if (col.GetComponent<Spawner> () == null &&
				   col.GetComponent<Projectile> () == null) 
				{
					_surgPrecAug.resetPrecisionCounter ();
				}
			
			}
		}

	}

	// Should be called when bullet despawns without hitting anything
	public void signalBulletDespawned()
	{
		_surgPrecAug.resetPrecisionCounter ();
	}

}
