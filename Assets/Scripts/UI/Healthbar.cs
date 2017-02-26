﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {
    public GameObject healthBar;

    public void UpdateHealthBar(float hp)
    {
        //Scales the foreground of the healthbar to the value of hp clamped between 0 and 1
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(hp, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
    

}
