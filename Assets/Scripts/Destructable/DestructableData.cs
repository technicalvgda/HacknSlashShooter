using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableData : MonoBehaviour {
    public float maxHealth;
    public Healthbar HPBar;
    public float health { get; private set; }

    void Start () {
        health = maxHealth;	
	} 

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (transform.tag == "Player")
        {
            HPBar.UpdateHealthBar(health / maxHealth);
        }
        if(health <= 0)
        {
            transform.gameObject.SetActive(false);
            if (transform.tag == "Player")
            {
                FindObjectOfType<Canvas>().enabled = true;
            }
        }
    }

    public void HealDamage(float recovery)
    {
        if (health < maxHealth)
        {
            //no overhealing
            if (health + recovery > maxHealth)
            {
                health = health + (recovery - ((health + recovery) - maxHealth));
            }
            else
            {
                health += recovery;
            }
        }
        HPBar.UpdateHealthBar(health / maxHealth);
    }

	
}
