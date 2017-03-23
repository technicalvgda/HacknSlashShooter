using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public GameObject[] weapons;
	public int currentWeapon = 0;

	public Weapon equipped;
	// Use this for initialization
	void Start () {
		SwitchWeapon (currentWeapon);
	}

	// Update is called once per frame
	void Update () {
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
