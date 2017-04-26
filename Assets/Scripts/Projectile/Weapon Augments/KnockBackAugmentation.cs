using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAugmentation : Augmentation
{


	private PlayerController _player;
	private WeaponManager _playerWeapon;
	// Use this for initialization
	void Start ()
	{
		_player = GetComponentInParent<PlayerController> ();
		_playerWeapon = GetComponentInParent<WeaponManager> ();
	}

	public override void augmentShot()
	{
		Debug.Log ("Augment shot");
	}

}
