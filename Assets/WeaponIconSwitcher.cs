using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconSwitcher : MonoBehaviour {
	public Sprite[] weaponIcons;
	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
        image.sprite = weaponIcons[0];
	}

	public void switchImage(int imageIndex)
	{
		image.sprite = weaponIcons[imageIndex];
	}
}
