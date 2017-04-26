using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Combo boost augmentation.
/// If the player can hit minCounter of enemies with one bullet, they get a temporary boost to their
/// movement speed and the fire rate of their weapons
/// 
/// Use by attaching this component to the weapon you want to augment
/// </summary>
public class ComboBoostAugmentation : Augmentation
{
	private PlayerController _player;
	private WeaponManager _weaponManager;

	public float movementMultiplier = 2;	
	public float rpmMultiplier = 2;	

	public int comboCounter = 0;
	public int minimumComboNeeded = 3;
	public float boostTime = 5; 			// Length of boost in secs

	private float _currBoostTime = 0;
	private bool _isBoosting = false;

	void Start()
	{
		_player = GetComponentInParent<PlayerController>();
		_weaponManager = GetComponentInParent<WeaponManager> ();
	}

	void Update()
	{
		if (_isBoosting) {
			_currBoostTime += Time.deltaTime;
			if (_currBoostTime >= boostTime) {
				_player.resetBoostMovementSpeed ();
				_weaponManager.stopBoostRPMofAllWeapons (rpmMultiplier);
				//Debug.Log ("Combo Ended");
				_isBoosting = false;
			}
		}
	}

	public override void augmentShot ()
	{
		//Debug.Log ("In Combo Mode! (AKA ComboBoostAugmentation Class)");
	}

	public void startBoosting()
	{
		_player.boostMovementSpeed (movementMultiplier);
		_weaponManager.boostRPMofAllWeapons (rpmMultiplier);
		_currBoostTime = 0;
		_isBoosting = true;
		//Debug.Log ("Combo Time!");
	}
}
