using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

	private PlayerData localPlayerData = new PlayerData();
		
	void Start()
	{
		setPlayerData (); // When player is instantiated, sets the player data from the global save 
	}
		
	/// <summary>
	/// Saves the player data to the global variable.
	/// 
	/// Call this before every scene change to retain player data
	/// </summary>
	public void saveToGlobalData()
	{
		setLocalPlayerData ();
		GlobalControl.Instance.savedPlayerData = localPlayerData;
	}

	/// <summary>
	/// Sets the player data from the saved player data from the global variable.
	/// 
	/// </summary>
	public void setPlayerData()
	{
		localPlayerData = GlobalControl.Instance.savedPlayerData;
		setPlayerDataFromLocalData ();

	}

	/// <summary>
	/// Sets the local player data based on the Player GameObject
	/// </summary>
	public void setLocalPlayerData()
	{
		localPlayerData.speed = GetComponent<PlayerController>().Speed;
		localPlayerData.speedMultiplier = GetComponent<PlayerController>().SpeedMultiplier;
		localPlayerData.hasComboBoostAug = GetComponentInChildren<ComboBoostAugmentation>(true).enabled;
		localPlayerData.hasKnockBackAug = GetComponentInChildren<KnockBackAugmentation>(true).enabled;
		localPlayerData.hasSurgicalPresAug = GetComponentInChildren<SurgicalPrecisionAugmentation>(true).enabled;
		localPlayerData.acquired = GetComponent<LaserBall>().acquired;
	}

	/// <summary>
	/// Translates the local player data to the actual Player GameObject
	/// </summary>
	public void setPlayerDataFromLocalData()
	{
		GetComponent<PlayerController>().Speed = localPlayerData.speed;
		GetComponent<PlayerController>().SpeedMultiplier =localPlayerData.speedMultiplier;
		GetComponentInChildren<ComboBoostAugmentation>(true).enabled = localPlayerData.hasComboBoostAug;
		GetComponentInChildren<KnockBackAugmentation>(true).enabled = localPlayerData.hasKnockBackAug;
		GetComponentInChildren<SurgicalPrecisionAugmentation> (true).enabled = localPlayerData.hasSurgicalPresAug;
		GetComponent<LaserBall>().acquired = localPlayerData.acquired;
	}
}
