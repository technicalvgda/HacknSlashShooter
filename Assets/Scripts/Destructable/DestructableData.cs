using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableData : MonoBehaviour {
    public float maxHealth;
    public float health {
        get; private set;
    }
    public bool SetHealth(float newHealth)
    {
        health = newHealth;
        if (health <= 0)
        {
            transform.gameObject.SetActive(false);
            if(transform.tag == "Player")
            {
                FindObjectOfType<Canvas>().enabled = true;
            }
            return false;
        }
        return true;
    }
	void Start () {
        health = maxHealth;	
	} 
	void Update () {
		
	}
}
