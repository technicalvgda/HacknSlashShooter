using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knock back augmentation.
/// After every shot, knocks the player away from where they are facing with a defined force
/// 
/// Use by attaching this component to the weapon you want to augment
/// </summary>
public class KnockBackAugmentation : Augmentation
{
	private PlayerController _player;

	public float knockBackForce = 100;

	void Start ()
	{
		_player = GetComponentInParent<PlayerController> ();
	}

	public override void augmentShot()
	{
		_player.playerKnockBack(knockBackForce);
		//Debug.Log ("Send Knock Back Player Message");
	}

}
