using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueNPC : MonoBehaviour {
    [SerializeField]
    private GameObject upgrade1, upgrade2;
    private bool rescued;
    public GameObject objectToUnlock; //a gate, path, platform, door, whatever is blocking you from the level end, if applicable
	// Use this for initialization
	void Start () {
        rescued = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!upgrade1.Equals(null) && !upgrade2.Equals(null))
        {
            if (upgrade1.GetComponent<BuffListHandler>().interacted || upgrade2.GetComponent<BuffListHandler>().interacted)
            {
                Destroy(upgrade1);
                Destroy(upgrade2);
            }
        }

    }
    /// <summary>
    /// When the player enters and presses E, they will cue the NPC to say something and present two options.
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerStay(Collider c)
    {
        //Some UI stuff with text popup talking to the player goes here
        if(c.gameObject.tag == "Player")
        {
            Debug.Log("entered");
            if (Input.GetKeyDown(KeyCode.E) )
            {
                if (!rescued && !upgrade1.Equals(null) && !upgrade2.Equals(null))
                {
                    //thanks for saving me, take this!
                    rescued = true;
                    upgrade1.SetActive(true);
                    upgrade2.SetActive(true);
                }
                else
                {
                    //thanks for the help, goodluck!
                }
            }
            
        }
    }
}
