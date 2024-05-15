using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialSkeleton : MonoBehaviour {

	public int startingHealth = 30;
	public int currentHealth;
	public Slider healthSlider;
	public Text healthText;
	public float speed=-2;
	public float attackRate=0.5f;
	public int damage=5;
	public float damageTakeRate=0.01f;
	public GameObject boneStructure;

	Animator anim;
	private float nextAttack=0;
	float nextTakeDamage=0;
	private bool dead=false;

	TutorialDamageScript tutorialDM;

	void Start () {
		tutorialDM = GameObject.FindWithTag ("Player").GetComponent<TutorialDamageScript> ();
		anim = GetComponent<Animator> ();

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
				tutorialDM.TakeDamage (damage);
			}
		} 
	}
	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetTrigger ("Attack");
			if (Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				tutorialDM.TakeDamage (damage);
			}
		} 
	} 

	void OnCollisionEnter2D (Collision2D other){       
		if (other.gameObject.tag == "death") {
			Destroy (this.gameObject);
		} 
	}

	public void Die(){
		speed = 0;
		Instantiate (boneStructure,transform.position,transform.rotation);
		Destroy (this.gameObject);
	}

}


