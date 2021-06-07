using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //damage image
    public Image damageImage;
    public float flashspeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    //UI
    public HealthBar hp;
    public Text TimeText;
    //health system
    public int MaxHealthSeconds = 120;
    public AnimationCurve DifficultyCurve;
    public static float TimePassed;
    [Range(0, 1)] public float Health = 1;
    public int health;
    private bool attacked;
    private float damage;
    //gameover
    public bool stopped;
    float restartDelay;
    public GameOver GameOver;
    //sound 
    public AudioClip GameOverClip;
    public AudioClip ThrowClip;
    public AudioClip HurtSound;
    AudioSource HurtAudio;
    public GameObject StepSound;
    void Start()
    {
        HurtAudio = GetComponent<AudioSource>();
        stopped = true;
        hp.SetMaxHealth(MaxHealthSeconds);
        hp.htext.text = MaxHealthSeconds+"";
        TimePassed = 0;
        restartDelay = 7f;
    }
    public void EnableStop()
	{
        stopped = false;
	}
   
    void Update()
    {
        if (Health <= 0) 
        {
            hp.htext.text = 0 + "";//keep the ui hp at 0
            HurtAudio.clip = GameOverClip;
            StepSound.SetActive(false);
            if (!HurtAudio.isPlaying &&!stopped)
            {
                HurtAudio.Play();//death sound once only
            }
            stopped = true; // disable the game
            GameOver.gameObject.SetActive(true);
            restartDelay -= Time.deltaTime;
            if(restartDelay<=0) DelayRestart();
        } 

        if (stopped) return; //game disabled

        TimePassed += Time.deltaTime; // time 

        if(attacked)
		{
            //attacked by an enemy
            Health -= damage;
            damageImage.color = flashColor;
            attacked = false;
            HurtAudio.clip = HurtSound;
            HurtAudio.Play();
        }
        else
		{
            Health -= Time.deltaTime / MaxHealthSeconds * DifficultyCurve.Evaluate(TimePassed);//hp diminishes
            damageImage.color = Color.Lerp(damageImage.color,Color.clear,flashspeed*Time.deltaTime); //damage image flash
        }
        health = (int)(MaxHealthSeconds * Health);//setup health for ui

        HealthUI();
    }
    private void OnTriggerStay(Collider other)
    {
        //objects thrown -> restore hp
		if (other.gameObject.tag.Equals("Throwable")&&!stopped)
		{
            if (other.transform.parent.GetComponent<Rigidbody>().isKinematic)
            {
                return;//ignore it
            }
            float health = other.transform.parent.gameObject.GetComponent<HealthAmount>().amountHealthAdded;
            HurtAudio.clip = ThrowClip;
            HurtAudio.Play();
            Heal(health);
            //delete the object thrown completely
            Destroy(other.gameObject);
            Destroy(other);
            Destroy(other.transform.parent.gameObject);
        }
    }
    void DelayRestart()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//reload a scene
    }

    public void Heal(float health)
	{
        Health += health;
        if (Health > 1) Health = 1;//prevent overflow
	}
    private void HealthUI()
	{
        hp.SetHealth(health);//update slider value
        hp.htext.text = health + "";//update text hp value
        int min = (int)TimePassed / 60;
        int sec = (int)TimePassed % 60;
        TimeText.text = $"{min:00} : {sec:00}"; // show time in min : sec
    }
    public void TakeDamage(float damage)
	{
        attacked = true;
        this.damage = damage;
    }
}
