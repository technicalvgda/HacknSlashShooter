using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float Speed;
    public float maxSpeed;
    public GameObject floor;

    private Canvas UI;
	private PlayerWeapon _playerWeapon;

    private float fireRateMult = 1.0f;

    CharacterController cc;
	// Use this for initialization
	void Start () {
        UI = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>();
		_playerWeapon = GetComponent<PlayerWeapon> ();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = GetMousePos();

        if (UI.enabled)
        {
            AngleUpdate(mousePos);
            Movement();
			if (Input.GetButton("Fire1"))
			{
            	_playerWeapon.ShootInput(GetAngle(transform.position, mousePos));
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

        float axisTotal = Mathf.Abs(xAxis) + Mathf.Abs(yAxis);
        float xInfluence = xAxis != 0 ? xAxis * (xAxis / axisTotal) * Mathf.Sign(xAxis) : 0;
        float yInfluence = yAxis != 0 ? yAxis * (yAxis / axisTotal) * Mathf.Sign(yAxis) : 0;
        Vector3 displacement = new Vector3(Speed * xInfluence, 0, Speed * yInfluence);
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
        _playerWeapon.RPM = fireRateMult * _playerWeapon.baseRPM;
    }
}
