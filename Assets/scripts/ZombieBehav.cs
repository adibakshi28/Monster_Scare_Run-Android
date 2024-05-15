using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieBehav : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Text healthText;
	public float speed=2;
	public float attackRate=0.5f;
	public int damage=2;
	public float damageTakeRate=0.01f;
	public int mvtDirection = -1;
	public GameObject[] powerups;

	private int enemyType=1;
	Vector3 lookRotation = new Vector3 (0, 0, -1);
	float nextTakeDamage=0;
	Animator anim;
	AudioSource aud;
	private float nextAttack=0;
	private bool dead=false;
	private bool IsGrounded=true;

	DamageScript dm;
	GameObject levelController;
	DataKeeper recordingScript ;
	LevelDataController levelControllerScript;

	void Start () {
		levelController=GameObject.FindWithTag ("levelDataController"); 
		dm = GameObject.FindWithTag ("Player").GetComponent<DamageScript> ();
		recordingScript = levelController.GetComponent<DataKeeper> ();
		levelControllerScript = levelController.GetComponent<LevelDataController> ();
		anim = GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();

		startingHealth = (int)(startingHealth * (levelControllerScript.enemyHealthMultiplier));
		damage = (int)(damage * (levelControllerScript.enemyDamageMultiplier));

		currentHealth = startingHealth;
		healthText.text = currentHealth.ToString ();
	}

	void Update () {
		anim.SetBool ("Grounded", IsGrounded);
		Walk (mvtDirection);
	}

	void Walk(int direction){
		if (direction > 0) {
			transform.rotation=Quaternion.identity;
		}
		else
		{
			Quaternion rotation = Quaternion.LookRotation(lookRotation);
			transform.rotation = rotation;
		}
		transform.Translate (speed*Time.deltaTime, 0, 0);  
	}  

	public void TakeDamage(int takeDamage){
		if (Time.time > nextTakeDamage) {
			nextTakeDamage = Time.time + damageTakeRate;
			currentHealth -= takeDamage;
			healthSlider.value = currentHealth;
			healthText.text = currentHealth.ToString ();
			if ((currentHealth <= 0)&&(!dead)) {
				Destroy (this.gameObject, 4);
				dead = true;
				recordingScript.EnemyKilledIncreasor (enemyType);
				anim.SetTrigger ("Dead");
				aud.Play ();
				PowerupCreator ();
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetTrigger ("Attack");
			if (Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				dm.TakeDamage (damage);
			}
		} 	
	}
	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetTrigger ("Attack");
			if (Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				dm.TakeDamage (damage);
			}
	    } 
	} 
	void OnTriggerExit2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetTrigger ("AttackStop");
		} 
	} 

	void OnCollisionExit2D (Collision2D other){       
		IsGrounded = false;
	}
	void OnCollisionStay2D (Collision2D other){       
		IsGrounded = true;
	}

	void OnCollisionEnter2D (Collision2D other){       
		if ((other.gameObject.tag == "death")&&(!dead)) {
			Destroy (this.gameObject);
		} 
	}

	void PowerupCreator(){
		if ((Random.Range (19, 91) % (8-(int)(PlayerPrefs.GetFloat("9")))==0)){
			Instantiate (powerups [Random.Range (0, powerups.GetLength (0))], this.gameObject.transform.position, Quaternion.identity);
		}
	}
}


