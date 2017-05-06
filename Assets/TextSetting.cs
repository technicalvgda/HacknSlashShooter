using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetting : MonoBehaviour {
    public Text text;

    private string[] descriptions = new string[]{"Hit 5 enemies without missing and recieve health.(Single Shot)",
        "Pierce through 3 enemies and gain an increased fire rate and movement speed.(Sniper)",
        "Firing will push you back a small distance.(Shotgun)",
        "Throw out a decoy that will attract enemies and detonate.",
        "Fire a massive laser blast in the direction you face."
    }; 

    public void SetText(BuffListHandler.Buffs b)
    {
        switch (b) {
            case BuffListHandler.Buffs.precisionaug:
                text.text = descriptions[0];
                break;
            case BuffListHandler.Buffs.comboaug:
                text.text = descriptions[1];
                break;
            case BuffListHandler.Buffs.kbaug:
                text.text = descriptions[2];
                break;
            case BuffListHandler.Buffs.decoy:
                text.text = descriptions[3];
                break;
            case BuffListHandler.Buffs.laser:
                text.text = descriptions[4];
                break;
            default:
                text.text = "You shouldn't see this, report to the nearest person.";
                break;                
        }
    }
}
