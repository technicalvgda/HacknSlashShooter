using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {
    public int slowDamage = 5;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.transform.tag == "Player")
        {
            other.GetComponent<DestructableData>().TakeDamage(slowDamage);
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            DestructableData x = this.GetComponent<DestructableData>();
            x.TakeDamage(x.health);
        }
        else if(other.transform.tag == "Objective")
        {
            other.GetComponentInChildren<DestructableData>().TakeDamage(5);
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            DestructableData x = this.GetComponent<DestructableData>();
            x.TakeDamage(x.health);
        }
    }
}
