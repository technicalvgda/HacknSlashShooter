using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAugmentation : Augmentation
{
	private PlayerController _player;
	// Use this for initialization
	void Start ()
	{
		_player = GetComponentInParent<PlayerController> ();
	}

	public override void augmentShot()
	{
		_player.playerKnockBack();
		Debug.Log ("Send Knock Back Player Message");
	}

}
