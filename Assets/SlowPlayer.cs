using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().Speed -= 1;
            if(other.GetComponent<PlayerController>().Speed == 0)
            {
                other.gameObject.SetActive(false);
            }
            //other.GetComponent<DestructableData>().SetHealth(other.GetComponent<PlayerController>().Speed);
            this.gameObject.SetActive(false); 
        }
    }
}
