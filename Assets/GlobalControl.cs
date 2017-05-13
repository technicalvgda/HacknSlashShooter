using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance;
	public PlayerData savedPlayerData = new PlayerData();

	void Awake ()   
	{
		// Makes sure there is only one instance of this class
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
		initializePlayerData ();
		Debug.Log ("Should only happen once");
	}

	/// <summary>
	/// Initializes the player data.
	/// 
	/// Call this to reset saved player data. Like when a player starts a new game.
	/// </summary>
	void initializePlayerData()
	{
		savedPlayerData.speed = 5;
		savedPlayerData.speedMultiplier = 1;
		savedPlayerData.hasComboBoostAug = false;
		savedPlayerData.hasKnockBackAug = false;
		savedPlayerData.hasSurgicalPresAug = false;
		savedPlayerData.acquired = new List<LaserBall.powertype>();

	}

}

