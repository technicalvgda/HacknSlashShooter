using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffListHandler : MonoBehaviour {
    //Just to make picking/switching them easier.
    public enum Buffs
    {
        health,
        firerate,
        decoy,
        laser,
    }

    public Buffs buffs;
    public bool interacted = false;
    private GameObject _player;
    private DestructableData _playerHP;
    public GameObject nameplate;
    /// <summary>
    /// Handles what stats increase what variables by whichever amount
    /// </summary>
    [System.Serializable]
    public class BuffStats
    {
        public float healthIncrease = 15.0f; //Flat health increase
        public float fireRateIncreasePercent = 0.25f; // percent our firerate increases by
    }

    public BuffStats buffList;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerHP = _player.GetComponent<DestructableData>();
        
    }

    //Handles the item nameplate when the player steps into its interactable area
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            nameplate.SetActive(true);
        }
    }
    void OnTriggerExit(Collider c){
        if (c.gameObject.tag == "Player")
        {
            nameplate.SetActive(false);
        }
    }
    /// <summary>
    /// Player enters and selects this buff by pressing E.
    /// toggles interacted to true to mark that this set of buffs has already been recieved.
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)){
                Debug.Log("interacting");
                //playerHP.HealDamage(9999.0f); //uncomment this to heal the player to full
                applyBuff();
                interacted = true;
            }
        }
    }
    /// <summary>
    /// Depending on the selected buff in the inspector, it will apply the changes to any relevant stat.
    /// 
    /// </summary>
    void applyBuff()
    {
        switch (buffs)
        {
            case Buffs.health:
                _playerHP.maxHealth += buffList.healthIncrease;
                _playerHP.HealDamage(buffList.healthIncrease);
                break;
            case Buffs.firerate:
                _player.GetComponent<PlayerController>().IncreaseStats(buffList.fireRateIncreasePercent);
                break;
            case Buffs.decoy:
                _player.GetComponent<LaserBall>().acquirePower(LaserBall.powertype.decoy);
                break;
            case Buffs.laser:
                _player.GetComponent<LaserBall>().acquirePower(LaserBall.powertype.laser);
                break;
            default:
                break;
        }
    }

}
