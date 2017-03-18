using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenButton : MonoBehaviour {
    public bool unlockDoor; //do we want to keep the door locked and kept open? or do we want the door to be simply unlocked.
    public GameObject door;
    private Door d;

    void Start()
    {
        d = door.GetComponentInChildren<Door>();
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            d.Open();
            if (unlockDoor)
            {
                d.Unlock();
            }
        }
    }
}
