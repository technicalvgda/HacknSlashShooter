using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : MonoBehaviour {
    //Just to make picking switching them easier.
    public enum Buffs
    {
        health,
        firerate
    }

    public Buffs buffs;
    private GameObject _player;
    private DestructableData _playerHP;
    //this is where the new stats will go

    [System.Serializable]
    public class BuffStats
    {
        public float healthIncrease = 15.0f; //Flat health increase
        public float fireRateIncreasePercent = 0.25f; // percent our firerate increases by
    }

    // Use this for initialization
    public BuffStats buffList;
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerHP = _player.GetComponent<DestructableData>();
        
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject == _player && Input.GetKeyDown(KeyCode.E))
        {
            //playerHP.HealDamage(9999.0f); //uncomment this to heal the player to full
            applyBuff();
        }
    }

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
            default:
                break;
        }
    }

}
