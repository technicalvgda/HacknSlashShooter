using System;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class DestructableData : MonoBehaviour {
    public float maxHealth;
    public Healthbar HPBar;
    public float health { get; private set; }
    public float hitFlashDelay = 0.1f;
    public Color color = Color.red;
    private Color _origColor;
    private Renderer render;


    private bool isDamaged = false;
    private int regenTimer = 0;
    public int regenDelay = 5;


    void Start () {
        health = maxHealth;
        //DELETE this if statement later. Should not be finalized
        if (transform.GetComponent<Renderer>() != null)
        {
            render = transform.GetComponent<Renderer>();
            _origColor = render.material.color;
        }
	} 

    public void TakeDamage(float damage)
    {
        //DELETE this if statement later. Should not be finalized
        if (transform.GetComponent<Renderer>() != null)
        {
            Timing.RunCoroutine(FlashColor());
        }
        health -= damage;
        if (transform.tag == "Player")
        {
            HPBar.UpdateHealthBar(health / maxHealth);
            regenTimer = 0;
            if (!isDamaged)
            {
                isDamaged = true;
                Timing.RunCoroutine(_regenHealth());
            }
        }
        if(health <= 0)
        {
            Destroy(transform.gameObject);
            if (transform.tag == "Player")
            {
                FindObjectOfType<Canvas>().enabled = true;
            }
        }
    }

    private IEnumerator<float> _regenHealth()
    {
        
        while (isDamaged)
        {
            while (regenTimer < regenDelay)
            {
                yield return Timing.WaitForSeconds(1.0f);
                regenTimer++;
            }
            if (health < maxHealth)
            {
                yield return Timing.WaitForSeconds(1.0f);
                health += 5;
                HPBar.UpdateHealthBar(health / maxHealth);
            }else if (health >= maxHealth)
            {
                health = maxHealth;
                HPBar.UpdateHealthBar(health / maxHealth);
                isDamaged = false;
            }
            
        }
    }

    private IEnumerator<float> FlashColor()
    {
        Debug.Log("CALLED");
        render.material.color = color;
        yield return Timing.WaitForSeconds(hitFlashDelay);
        render.material.color = _origColor;
        yield return 0.0f;
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
        if (HPBar != null)
        {
            HPBar.UpdateHealthBar(health / maxHealth);
        }
    }

	
}
