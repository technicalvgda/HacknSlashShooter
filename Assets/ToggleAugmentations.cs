using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Toggle augmentations.
/// 
/// This is just for testing, don't add to final implementation
/// </summary>
public class ToggleAugmentations : MonoBehaviour 
{
	GameObject pistol;
	GameObject sniper;
	GameObject shotgun;



	void Start()
	{
		pistol = transform.Find ("Single Shot Gun").gameObject;
		sniper = transform.Find ("Sniper Gun").gameObject;
		shotgun = transform.Find ("Spread Shot Gun").gameObject;

	}
		
	void Update () 
	{
		
		if (Input.GetButtonDown("ToggleSPAug")) 
		{
			if (pistol.GetComponent<SurgicalPrecisionAugmentation> () == null) 
			{
				pistol.AddComponent<SurgicalPrecisionAugmentation> ();
			} 
			else 
			{
				pistol.GetComponent<SurgicalPrecisionAugmentation> ().enabled = !pistol.GetComponent<SurgicalPrecisionAugmentation> ().enabled;
			}
		}
		if (Input.GetButtonDown("ToggleCBAug")) 
		{
			if (sniper.GetComponent<ComboBoostAugmentation> () == null) 
			{
				sniper.AddComponent<ComboBoostAugmentation> ();
			} 
			else 
			{
				sniper.GetComponent<ComboBoostAugmentation> ().enabled = !sniper.GetComponent<ComboBoostAugmentation> ().enabled;
			}
		}
		if (Input.GetButtonDown("ToggleKBAug")) 
		{
			if (shotgun.GetComponent<KnockBackAugmentation> () == null) 
			{
				shotgun.AddComponent<KnockBackAugmentation> ();
			} 
			else 
			{
				shotgun.GetComponent<KnockBackAugmentation> ().enabled = !shotgun.GetComponent<KnockBackAugmentation> ().enabled;
			}
		}
		
	}
}
