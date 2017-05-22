using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool interacted = false;
    private GameObject _player;
    public GameObject nameplate;
    public GameObject interactText;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            nameplate.SetActive(true);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            nameplate.SetActive(false);
            interactText.SetActive(false);
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("interacting");
                interactText.SetActive(!interactText.active);
            }
        }
    }
}
