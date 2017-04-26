using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBoostBullet : MonoBehaviour {
	public int comboCounter = 0;
	public int minimumComboNeeded = 3;

	private ComboBoostAugmentation _comboAug;

	void Start()
	{
		_comboAug = GetComponent<Projectile> ().owner.GetComponentInChildren<ComboBoostAugmentation> ();
	}

	void OnEnable()
	{
		comboCounter = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<DestructableData> () != null) {
			comboCounter++;
			Debug.Log (comboCounter);
		}
	}

	void Update(){
		if (comboCounter >= minimumComboNeeded) {
			comboCounter = 0;
			_comboAug.startBoosting ();
		}

	}
}
