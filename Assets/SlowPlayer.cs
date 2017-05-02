using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {
    public int slowDamage = 5;
    DestructableData x;
    void Start()
    {
        x = this.GetComponent<DestructableData>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<DestructableData>().TakeDamage(slowDamage);
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            x.TakeDamage(x.health);
        }
        else if(other.transform.tag == "Objective")
        {
            other.GetComponentInChildren<DestructableData>().TakeDamage(5);
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            x.TakeDamage(x.health);
        }
    }
}
