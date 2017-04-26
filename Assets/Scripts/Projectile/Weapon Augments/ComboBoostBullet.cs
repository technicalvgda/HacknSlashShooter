using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Combo boost bullet.
/// 
/// Handles the collision logic for the Combo boost augmentation.
/// </summary>
public class ComboBoostBullet : MonoBehaviour {
	private int _comboCounter = 0;
	private int _minimumComboNeeded = 3;

	private ComboBoostAugmentation _comboAug;

	void Start()
	{
		_comboAug = GetComponent<Projectile> ().owner.GetComponentInChildren<ComboBoostAugmentation> ();
		_comboCounter = _comboAug.comboCounter;
		_minimumComboNeeded = _comboAug.minimumComboNeeded;
	}

	void OnEnable()
	{
		_comboCounter = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<DestructableData> () != null) {
			_comboCounter++;
			//Debug.Log (comboCounter);
		}
	}

	void Update(){
		if (_comboCounter >= _minimumComboNeeded) {
			_comboCounter = 0;
			_comboAug.startBoosting ();
		}

	}
}
