using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialZombie : MonoBehaviour {

	public int startingHealth = 20;
	public int currentHealth;
	public Slider healthSlider;
	public Text healthText;
	public float speed=2;
	public float attackRate=0.5f;
	public int damage=5;
	public float damageTakeRate=0.01f;
	public int mvtDirection = -1;
	public GameObject[] powerups;

	Vector3 lookRotation = new Vector3 (0, 0, -1);
	float nextTakeDamage=0;
	Animator anim;
	private float nextAttack=0;
	private bool dead=false;
	private bool IsGrounded=true;

	TutorialDamageScript tutorialDM;

	void Start () {
		tutorialDM = GameObject.FindWithTag ("Player").GetComponent<TutorialDamageScript> ();
		anim = GetComponent<Animator> ();

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
				anim.SetTrigger ("Dead");
			}
		}
	}

	public void Die(){
		Destroy (this.gameObject, 4);
		dead = true;
		anim.SetTrigger ("Dead");
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
}


