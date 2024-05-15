using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkeletonBehav : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Text healthText;
	public float speed=-2;
	public float attackRate=0.5f;
	public int damage=2;
	public float damageTakeRate=0.01f;
	public GameObject boneStructure;

	private int enemyType = 2;
	Animator anim;
	private float nextAttack=0;
	float nextTakeDamage=0;
	private bool dead=false;

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

		startingHealth = (int)(startingHealth * (levelControllerScript.enemyHealthMultiplier));
		damage = (int)(damage * (levelControllerScript.enemyDamageMultiplier));

		currentHealth = startingHealth;
		healthText.text = currentHealth.ToString ();
	}
		
	void Update(){
		transform.Translate (speed*Time.deltaTime, 0, 0); 
		if (dead) {
			Destroy (this.gameObject);
		}
	}

	public void TakeDamage(int takeDamage){
		if (Time.time > nextTakeDamage) {
			nextTakeDamage = Time.time + damageTakeRate;
			currentHealth -= takeDamage;
			healthSlider.value = currentHealth;
			healthText.text = currentHealth.ToString ();
			if ((currentHealth <= 0)&&(!dead)) {
				anim.SetTrigger ("Dead");
				dead = true;
				Die ();
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

	void OnCollisionEnter2D (Collision2D other){       
		if (other.gameObject.tag == "death") {
			Destroy (this.gameObject);
		} 
	}

	void Die(){
	    speed = 0;
		recordingScript.EnemyKilledIncreasor (enemyType);
		Instantiate (boneStructure,transform.position,transform.rotation);
	}

}


