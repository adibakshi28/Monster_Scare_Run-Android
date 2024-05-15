using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageScript : MonoBehaviour {
	
		public int startingHealth = 100;
		public int currentHealth;
		public AudioClip heartBeat;
		public float flashSpeed = 5f;
		public Color flashColour = new Color(1f, 0f, 0f, 0.2f);

	 Slider healthSlider;
	 Image damageImage;
	 Text healthText;	
	AudioSource playerAudio;
//	    Animator anim;
	 Animator anim;
	    LevelDataController levelDataScript;
//	    RobotController playerMovementScript;
	PlayerScript playerMovementScript;
	bool damaged,dead=false;


		void Start ()
		{
			playerAudio = GetComponent <AudioSource> ();
		    levelDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<LevelDataController> ();
		    playerMovementScript=GetComponent<PlayerScript>();
	     	healthSlider = levelDataScript.playerHealthSlider;
		    damageImage = levelDataScript.playerDamageImage;
		    healthText = levelDataScript.playerHealthText;
		    anim=GetComponent<Animator>();
		    healthSlider.maxValue=(int)(PlayerPrefs.GetFloat("0"));
		    startingHealth=(int)(PlayerPrefs.GetFloat("0"));
		    currentHealth = startingHealth;
		    healthText.text = currentHealth.ToString ();
		}


		void Update ()
		{
			if(damaged)
			{
				damageImage.color = flashColour;
			}
			else
			{
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			}
			damaged = false;

		}


		public void TakeDamage (int amount)
	{
		if (!dead) {
			damaged = true;
			currentHealth -= amount;
			Handheld.Vibrate ();
			healthSlider.value = currentHealth;
			healthText.text = currentHealth.ToString ();
			playerAudio.clip = heartBeat;
			playerAudio.Play ();
			if (currentHealth < 25) {
				levelDataScript.HeartBlinker ();
			} 

			if (currentHealth <= 0) {
	//			healthText.text = "0";
				Death ();
			}  
		}
	}
	   
	public void IncreaseHealth (int amount)
	{
		if (startingHealth > currentHealth + amount) {
			currentHealth = currentHealth + amount;	    
		} else {
		   currentHealth = startingHealth;
		}

			healthSlider.value = currentHealth;
		    healthText.text = currentHealth.ToString ();
			if (currentHealth > 25) {
				levelDataScript.HeartBlinkerStopper ();
			}
		}


	void Death ()
	{
		if (!dead) {
			healthText.text = "0";                       // Death by falling causes health to go in negitive 
			playerMovementScript.enabled = false;
			dead = true;
			anim.SetTrigger ("Die");
			levelDataScript.GameEnd (); 
		}
	}
	}
