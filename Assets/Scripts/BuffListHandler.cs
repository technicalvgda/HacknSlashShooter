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
        comboaug,
        kbaug,
        precisionaug,
    }

    public Buffs buffs;
    public bool interacted = false;
    private GameObject _player;
    private DestructableData _playerHP;
    public GameObject nameplate;
    public GameObject augtext;
    public GameObject pwrtext;
    public GameObject descripttext;

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
            descripttext.GetComponent<TextSetting>().SetText(buffs);
            descripttext.SetActive(true);
            if (buffs == Buffs.comboaug || buffs == Buffs.kbaug || buffs == Buffs.precisionaug)
            {
                augtext.SetActive(true);
            }
            else if(buffs == Buffs.laser || buffs == Buffs.decoy)
            {
                pwrtext.SetActive(true);

            }

        }
    }
    void OnTriggerExit(Collider c){
        if (c.gameObject.tag == "Player")
        {
            nameplate.SetActive(false);
            augtext.SetActive(false);
            pwrtext.SetActive(false);
            descripttext.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Space)){
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
        augtext.SetActive(false);
        pwrtext.SetActive(false);
        descripttext.SetActive(false);
        switch (buffs)
        {
            case Buffs.decoy:
                _player.GetComponent<LaserBall>().acquirePower(LaserBall.powertype.decoy);
                break;
            case Buffs.laser:
                _player.GetComponent<LaserBall>().acquirePower(LaserBall.powertype.laser);
                break;
            case Buffs.comboaug:
                _player.GetComponent<ToggleAugmentations>().ToggleSniper();
                break;
            case Buffs.kbaug:
                _player.GetComponent<ToggleAugmentations>().ToggleShotgun();
                break;
            case Buffs.precisionaug:
                _player.GetComponent<ToggleAugmentations>().TogglePistol();
                break;
            default:
                break;
        }
    }

}
