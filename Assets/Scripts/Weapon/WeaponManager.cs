using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {
	
	public Weapon[] weapons;
	public int currentWeapon = 0;
	public WeaponIconSwitcher iconSwitcher;

	public Weapon equipped;
	// Use this for initialization
	void Start () {
		SwitchWeapon (currentWeapon);
	}

	// Update is called once per frame
	void Update () {
		// Switch weapon with Mouse Wheel
		if (Input.GetAxis("SwitchWeapon") != 0 || Input.GetButtonDown("SwitchWeapon")) 
		{
			currentWeapon = Mod((currentWeapon + (int)Input.GetAxis ("SwitchWeapon")), weapons.Length);
			SwitchWeapon (currentWeapon);
		}
		// Switch Weapons with Q and E
		if (Input.GetButtonDown ("SwitchWeaponUp")) 
		{
			currentWeapon = Mod((currentWeapon - 1), weapons.Length);
			SwitchWeapon (currentWeapon);
			
		}
		if (Input.GetButtonDown ("SwitchWeaponDown")) 
		{
			currentWeapon = Mod((currentWeapon + 1), weapons.Length);
			SwitchWeapon (currentWeapon);

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
		iconSwitcher.switchImage(index);
	}

	public void boostRPMofAllWeapons(float rmpMultiplier)
	{
		foreach (Weapon weapon in weapons) {
			weapon.boostRPM (rmpMultiplier);
		}
	}

	public void stopBoostRPMofAllWeapons()
	{
		foreach (Weapon weapon in weapons) {
			weapon.resetRPM ();
		}
	}

	/// <summary>
	/// Find the modulus of two numbers
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The blue component.</param>
	public static int Mod(float a, float b)
	{
		return (int)(a - b * Mathf.Floor (a / b));
	}
}
