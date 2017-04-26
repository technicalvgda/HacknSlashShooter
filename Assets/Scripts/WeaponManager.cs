using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
	
	public Weapon[] weapons;
	public int currentWeapon = 0;

	public Weapon equipped;
	// Use this for initialization
	void Start () {
		SwitchWeapon (currentWeapon);
	}

	// Update is called once per frame
	void Update () {

		//This tests a way to boost weapon fire rate from elsewhere
		if (Input.GetButtonDown ("Fire3")) {
			foreach (Weapon weapon in weapons) {
				GetComponentInParent<PlayerController> ().boostMovementSpeed (2);
				weapon.boostRPM (2);
			}
		}
		if (Input.GetButtonDown ("Fire4"))
		{
			foreach (Weapon weapon in weapons) {
				GetComponentInParent<PlayerController> ().resetBoostMovementSpeed ();
				weapon.resetRPM ();
			}
		}


		for (int i = 1; i <= weapons.Length; i++) {
			if(Input.GetKeyDown("" + i)){
				currentWeapon = i - 1;
				SwitchWeapon (currentWeapon);
			}
		}
	}
		
	void SwitchWeapon(int index){
		equipped = weapons [index].GetComponent<Weapon> ();
		for (int i = 0; i < weapons.Length; i++) {
			if (i == index) {
				weapons [i].gameObject.SetActive (true);
			} else {
				weapons [i].gameObject.SetActive (false);
			}
		}
	}
}
