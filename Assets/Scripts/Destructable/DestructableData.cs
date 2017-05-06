using System;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructableData : MonoBehaviour {
    public int pointValue = 0;
    public float maxHealth;
    public Healthbar HPBar;
    public float health { get; private set; }
    public float hitFlashDelay = 0.1f;
    public Color color = Color.red;

    private Color _origColor;
    private Renderer render;

    public GameObject gameover;
    public Animator gameOverF;

    private bool isDamaged = false;
    private int regenTimer = 0;
    public int regenDelay = 5;
    private bool checkedKill = false;


    void Start () {
        health = maxHealth;
        //DELETE this if statement later. Should not be finalized
        if (transform.GetComponent<Renderer>() != null)
        {
            render = transform.GetComponent<Renderer>();
            _origColor = render.material.color;
        }
	}

    public void TakeDamage(float damage, bool isPlayerDamage)
    {
        //DELETE this if statement later. Should not be finalized
        if (transform.GetComponent<Renderer>() != null)
        {
            Timing.RunCoroutine(FlashColor());
        }
        health -= damage;
        if (transform.tag == "Player" || transform.tag == "Objective")
        {
            HPBar.UpdateHealthBar(health / maxHealth);
            regenTimer = 0;
            if (!isDamaged)
            {
                isDamaged = true;
                Timing.RunCoroutine(_regenHealth());
            }
        }
        if (health <= 0)
        {
            if (GetComponent<WaveEnemy>() && !checkedKill)
            {
                checkedKill = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numKilled++;
                if (ScoreHandler.s != null && isPlayerDamage)
                {
                    Debug.Log("Adding Score");
                    ScoreHandler.s.AddScore(pointValue);
                }
            }
            if (transform.tag == "Player")
            {
                //gameover.SetActive(true);
                gameOverF.SetTrigger("Enter");
                if (SceneManager.GetActiveScene().name.Contains("Arena"))
                {
                    if (ScoreHandler.s != null)
                    {
                        ScoreHandler.s.RecordScore();
                        var scoreList = SaveHandler.s.GetScores();
                        if (scoreList == null)
                        {
                            Debug.Log("ScoreList == null");
                            SaveHandler.s.InitializeScores();
                            ScoreHandler.s.RecordScore();
                            scoreList = SaveHandler.s.GetScores();
                        }
                        Debug.Log("scorelist: " + scoreList);
                        var s = "High Scores: ";
                        for (int i = 0; i < scoreList.Count; i++)
                        {
                            s += scoreList[i] + " ";
                        }
                        Debug.Log(s);
                    }
                }
                //GameObject pause = GetComponent<PlayerController>().pause;
            }
            Destroy(transform.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        //DELETE this if statement later. Should not be finalized
        if (transform.GetComponent<Renderer>() != null)
        {
            Timing.RunCoroutine(FlashColor());
        }
        health -= damage;
        if (transform.tag == "Player" || transform.tag == "Objective")
        {
            HPBar.UpdateHealthBar(health / maxHealth);
            regenTimer = 0;
            if (!isDamaged)
            {
                isDamaged = true;
                Timing.RunCoroutine(_regenHealth());
            }
        }
        if(health <= 0)
        {
            if (GetComponent<WaveEnemy>() && !checkedKill)
            {
                checkedKill = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().numKilled++;
            }
            if (transform.tag == "Player")
            {
                //gameover.SetActive(true);
                gameOverF.SetTrigger("Enter");
                if (SceneManager.GetActiveScene().name.Contains("Arena"))
                {
                    if (ScoreHandler.s != null)
                    {
                        ScoreHandler.s.RecordScore();
                        var scoreList = SaveHandler.s.GetScores();
                        if (scoreList == null)
                        {
                            SaveHandler.s.InitializeScores();
                            ScoreHandler.s.RecordScore();
                            scoreList = SaveHandler.s.GetScores();
                        }
                        var s = "High Scores: ";
                        for (int i = 0; i < scoreList.Count; i++)
                        {
                            s += scoreList[i] + " ";
                        }
                        Debug.Log(s);
                    }
                }
                //GameObject pause = GetComponent<PlayerController>().pause;
            }
            if (GetComponent<SlowPlayer>())
            {
                GetComponent<SlowPlayer>().enabled = false;
            }
            if (GetComponent<bullChase>())
            {
                GetComponentInChildren<BullAnimationController>().Die();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator<float> _regenHealth()
    {
        
        while (isDamaged)
        {
            while (regenTimer < regenDelay)
            {
                yield return Timing.WaitForSeconds(1.0f);
                regenTimer++;
            }
            if (health < maxHealth)
            {
                yield return Timing.WaitForSeconds(1.0f);
                health += 5;
                HPBar.UpdateHealthBar(health / maxHealth);
            }else if (health >= maxHealth)
            {
                health = maxHealth;
                HPBar.UpdateHealthBar(health / maxHealth);
                isDamaged = false;
            }
            
        }
    }

    private IEnumerator<float> FlashColor()
    {
        render.material.color = color;
        yield return Timing.WaitForSeconds(hitFlashDelay);
        render.material.color = _origColor;
        yield return 0.0f;
    }

    public void HealDamage(float recovery)
    {
        if (health < maxHealth)
        {
            //no overhealing
            if (health + recovery > maxHealth)
            {
                health = health + (recovery - ((health + recovery) - maxHealth));
            }
            else
            {
                health += recovery;
            }
        }
        if (HPBar != null)
        {
            HPBar.UpdateHealthBar(health / maxHealth);
        }
    }

	
}
