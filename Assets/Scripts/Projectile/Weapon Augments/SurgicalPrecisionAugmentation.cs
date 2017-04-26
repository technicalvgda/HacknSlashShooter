using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Surgical precision augmentation.
/// If the player can hit maxCounter enemies without missing, they get healed some amount
/// 
/// Use by attaching this component to the weapon you want to augment
/// </summary>
public class SurgicalPrecisionAugmentation : Augmentation {

	public int maxCounter = 5;
	public int healAmount = 10;
	private int precisionCounter = 0;

	private DestructableData _health;

	void Start()
	{
		_health = GetComponentInParent<DestructableData> ();
	}

	public override void augmentShot ()
	{
		//Debug.Log ("Surgical Precision Mode!");
	}


	/// <summary>
	/// Increments the precision counter.
	/// Heals the owner for some amount
	/// </summary>
	public void incrementPrecisionCounter()
	{
		precisionCounter++;
		Debug.Log ("Precision counter: " + precisionCounter);
		if (precisionCounter >= maxCounter) 
		{
			_health.HealDamage(healAmount);
			resetPrecisionCounter ();
		}
	}

	/// <summary>
	/// Resets the precision counter.
	/// </summary>
	public void resetPrecisionCounter()
	{
		Debug.Log ("Reset Precision Counter");
		precisionCounter = 0;
	}
}
