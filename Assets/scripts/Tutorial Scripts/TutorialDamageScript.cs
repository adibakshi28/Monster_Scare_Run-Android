using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialDamageScript : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public AudioClip heartBeat;
	public float flashSpeed = 1f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.2f);

	Slider healthSlider;
	Image damageImage;
	Text healthText;	
	AudioSource playerAudio;
	TutorialDataController tutorialDataScript;
	bool damaged;


	void Start ()
	{
		playerAudio = GetComponent <AudioSource> ();
		tutorialDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<TutorialDataController> ();
		healthSlider = tutorialDataScript.playerHealthSlider;
		damageImage = tutorialDataScript.playerDamageImage;
		healthText = tutorialDataScript.playerHealthText;
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
			damaged = true;
			currentHealth -= amount;
			Handheld.Vibrate ();
			healthSlider.value = currentHealth;
			healthText.text = currentHealth.ToString ();
			playerAudio.clip = heartBeat;
			playerAudio.Play ();

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
	}
}
