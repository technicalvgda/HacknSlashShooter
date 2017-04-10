using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.transform.tag == "Player")
        {
            other.GetComponent<DestructableData>().TakeDamage(5);
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            DestructableData x = this.GetComponent<DestructableData>();
            x.TakeDamage(x.health);

        }
    }
}
