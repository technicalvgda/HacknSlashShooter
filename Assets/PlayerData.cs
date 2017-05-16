using System.Collections.Generic;

public class PlayerData {
	public float speed;
	public float speedMultiplier;
	public bool hasKnockBackAug;
	public bool hasSurgicalPresAug;
	public bool hasComboBoostAug;
	public List<LaserBall.powertype> acquired;

	// Look at values in the class
	public string ToString()
	{
		return string.Format ("Speed: {0}, Speed Multiplier: {1}, Has KnockBackAug: {2}, Has Surgical Pres Aug: {3}, Has Combo Boost Aug: {4}", speed, speedMultiplier, hasKnockBackAug, hasSurgicalPresAug, hasComboBoostAug);
	}
}
