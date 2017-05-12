using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using UnityEngine.SceneManagement;

public class LaserBall : MonoBehaviour {
    public GameObject throwmarker;
    public GameObject decoy;
    private Vector3 _heightOfDecoy = new Vector3(0, 0.1f, 0);
    public GameObject bullet;
    public float throwRange = 7.5f;
    public GameObject LaserUI, DecoyUI;
    private GameObject player;
    private RaycastHit hit;
    private Vector3 direction;
    private Vector3 mousePos;
    private PlayerController pc;

    private bool decoyCool = false;
    private bool laserCool = false;
    public float decoyCoolTime = 5;
    public float laserCoolTime = 5;

    public powertype powerup = powertype.none;

    /*void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.activeSceneChanged += setArena;
    }

    void setArena(Scene previousScene, Scene newScene)
    {
        Debug.Log(newScene.name);
        
        
        if(newScene.name == "Arena")
        {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            this.gameObject.GetComponent<PlayerController>().pause = GameObject.FindGameObjectWithTag("Pause");
            this.gameObject.GetComponent<DestructableData>().HPBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Healthbar>();
            this.gameObject.GetComponent<DestructableData>().gameover = GameObject.FindGameObjectWithTag("GameOver");
            this.gameObject.GetComponent<WeaponManager>().iconSwitcher = GameObject.FindGameObjectWithTag("BulletIcon").GetComponent<WeaponIconSwitcher>();
            LaserUI = GameObject.FindGameObjectWithTag("LaserUI");
            DecoyUI = GameObject.FindGameObjectWithTag("DecoyUI");
        }

        
    }*/
    

    public enum powertype
    {
        none,
        decoy,
        laser,
    }
    public List<powertype> acquired = new List<powertype>();
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
        pc = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2") && acquired.Contains(powertype.laser) && !laserCool)
        {
            Fire();
            laserCool = true;
            Timing.RunCoroutine(CoolDown(laserCoolTime, powertype.laser));
        }
        if (Input.GetButton("Fire2") && !GameObject.Find("DEcoy(Clone)") && acquired.Contains(powertype.decoy) && !decoyCool)
        {
            mousePos = PlayerController.GetMousePos();

            direction = mousePos - player.transform.position;
            if (Physics.Raycast(player.transform.position, direction, out hit, throwRange))
            {
                Debug.DrawLine(player.transform.position, hit.point);
                if (hit.collider.gameObject.tag == "Ground")
                {
                    GameObject m = Instantiate(throwmarker, mousePos, transform.rotation);
                    Destroy(m, 0.5f);
                    GameObject d = Instantiate(decoy, transform.position, transform.rotation);
                    d.GetComponent<ProjectionPowerup>().Activate(m.transform.position + _heightOfDecoy);
                }
            }
            decoyCool = true;
            Timing.RunCoroutine(CoolDown(decoyCoolTime, powertype.decoy));
        }

    }

    //This is where the UI will get updated when you acquire a new power
    public void acquirePower(powertype c)
    {
        if(c == powertype.decoy)
        {
            DecoyUI.SetActive(true);
            acquired.Add(c);
        }
        else if(c == powertype.laser)
        {
            LaserUI.SetActive(true);
            acquired.Add(c);
        }
    }

    void Fire()
    {
        Instantiate(bullet, player.transform.position, player.transform.rotation, this.transform);
    }

    IEnumerator<float> CoolDown(float time, powertype c)
    {
        yield return Timing.WaitForSeconds(time);
        if(c == powertype.laser)
        {
            laserCool = false;
        }
        if(c == powertype.decoy)
        {
            decoyCool = false;
        }
    }
}
