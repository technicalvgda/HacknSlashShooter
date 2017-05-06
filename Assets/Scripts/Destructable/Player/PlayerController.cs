using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //sound
    public AudioClip footstepSound;
    private AudioSource source;
    //endsound

    public float Speed;
    public float SpeedMultiplier;
	private float boostSpeedMultiplier = 1;
	private Vector3 impact = Vector3.zero;
	private float mass = 3;

    public GameObject pause;

    private Canvas UI;
	private WeaponManager _playerWeapon;
    private DestructableData _playerData;


	private float fireRateMult = 1.0f;

	public float multiplier{ get { return fireRateMult; }}

    public int numKilled;

	CharacterController cc;

	// Use this for initialization
	void Start () {
        //sound
        source = GetComponent<AudioSource>();

        //endsound
        Time.timeScale = 1;
        numKilled = 0;
        //UI = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>();
		_playerWeapon = GetComponent<WeaponManager> ();
        _playerData = GetComponent<DestructableData>();
        SpeedMultiplier = 1;
  }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = GetMousePos();
        SpeedMultiplier = _playerData.health / _playerData.maxHealth;

		
			AngleUpdate(mousePos);
			Movement();
            //sound
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else
            {
                source.Stop();
                //endsound
            }
            if (Input.GetButton("Fire1"))
			{
				_playerWeapon.equipped.ShootInput(GetAngle(transform.position, mousePos));
			}
		
		MenuControls();
	}

	void AngleUpdate(Vector3 tpos)
	{
		Vector3 pos = transform.position;
		transform.rotation = Quaternion.Euler(0, -GetAngle(pos, tpos) * 180f/Mathf.PI, 0);
	}

	public static float GetAngle(Vector3 v1, Vector3 v2)
	{
		return Mathf.Atan2(v2.z - v1.z, v2.x - v1.x);
	}

	public static Vector3 GetMousePos()
	{
        /*RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll(ray);

		foreach (RaycastHit rh in hits)
		{
			if (rh.collider.gameObject == floor)
			{

				return rh.point;
			}
		}
		return Vector3.zero;*///previous player direction control

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if(hPlane.Raycast(ray,out distance))
        {
            return (ray.GetPoint(distance));
        }
        return Vector3.zero;
    }

    void Movement()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");
		Vector3 displacement = new Vector3(xAxis, 0, yAxis).normalized * Speed * SpeedMultiplier * boostSpeedMultiplier;
        displacement.y -= 100f * Time.deltaTime;
        cc = cc == null ? GetComponent<CharacterController>() : cc;
        if (cc != null)
        {
            cc.Move(displacement * Time.deltaTime);
        }
		//Adds the displacement of the impact force to the Character Controller
		if (impact.magnitude > 0.2) 
		{
			cc.Move(impact * Time.deltaTime);
			impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
		}

    }

	/// <summary>
	/// Calculates the impact velocity on a Character Controller
	/// </summary>
	/// <param name="direction">Direction of the impact.</param>
	/// <param name="force">Force of the impact.</param>
	void AddImpact(Vector3 direction, float force)
	{
		impact += direction.normalized * force / mass;

	}

    void MenuControls()
    {
        return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pause.active)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            }else
            {
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    //Rewrite this later to accept mroe upgrades.
    public void IncreaseStats (float amount)
    {
        //additive, not multiplicative
        fireRateMult += amount;
        _playerWeapon.equipped.RPM = fireRateMult * _playerWeapon.equipped.baseRPM;
    }

	public void boostMovementSpeed(float multiplier)
	{
		//Debug.Log ("Speed Boost!");
		boostSpeedMultiplier = multiplier;
	}

	public void resetBoostMovementSpeed()
	{
		boostSpeedMultiplier = 1;
	}

	// knocks back a player away from the mouse position
	public void playerKnockBack(float knockBackForce)
	{
		AddImpact (transform.position - GetMousePos (), knockBackForce);
		//Debug.Log ("In player method knockback");
	}


	void loadPlayer(){
	}

}
