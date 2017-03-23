using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed;
    public float SpeedMultiplier;

    public GameObject floor;

    private Canvas UI;
	  private WeaponManager _playerWeapon;
    private DestructableData _playerData;


	private float fireRateMult = 1.0f;

	public float multiplier{ get { return fireRateMult; }}

	CharacterController cc;
	// Use this for initialization
	void Start () {

        UI = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>();
		_playerWeapon = GetComponent<WeaponManager> ();
        _playerData = GetComponent<DestructableData>();
        SpeedMultiplier = 1;
  }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = GetMousePos();
        SpeedMultiplier = _playerData.health / _playerData.maxHealth;
  }

		if (UI.enabled)
		{
			AngleUpdate(mousePos);
			Movement();
			if (Input.GetButton("Fire1"))
			{
				_playerWeapon.equipped.ShootInput(GetAngle(transform.position, mousePos));
			}
		}
		MenuControls();
	}

	void AngleUpdate(Vector3 tpos)
	{
		Vector3 pos = transform.position;
		transform.rotation = Quaternion.Euler(0, -GetAngle(pos, tpos) * 180f/Mathf.PI, 0);
	}

	float GetAngle(Vector3 v1, Vector3 v2)
	{
		return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
	}

	Vector3 GetMousePos()
	{
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll(ray);

		foreach (RaycastHit rh in hits)
		{
			if (rh.collider.gameObject == floor)
			{

				return rh.point;
			}
		}
		return Vector3.zero;
	}
	void Movement()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");
		Vector3 displacement = new Vector3(xAxis, 0, yAxis).normalized * Speed * SpeedMultiplier;
        displacement.y -= 100f * Time.deltaTime;
        cc = cc == null ? GetComponent<CharacterController>() : cc;
        if (cc != null)
        {
            cc.Move(displacement * Time.deltaTime);
            
            //transform.position += displacement * Time.deltaTime;
        }
        

    }

    void MenuControls()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>().enabled = !GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>().enabled;
        }
    }

    //Rewrite this later to accept mroe upgrades.
    public void IncreaseStats (float amount)
    {
        //additive, not multiplicative
        fireRateMult += amount;
        _playerWeapon.equipped.RPM = fireRateMult * _playerWeapon.equipped.baseRPM;
    }


}
