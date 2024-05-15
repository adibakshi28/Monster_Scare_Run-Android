using UnityEngine;
using System.Collections;

public class HangingSpider : MonoBehaviour {
	
	public float ossilationTime;
	public Vector2 speed;
	public float waitTime=1.5f;
	public float attackRate=0.5f;
	public int damage=2;
	public GameObject upLight;
	public GameObject downLight;

	private int enemyType = 3;
	Rigidbody2D rb;
	Vector2 zeroSpeed=new Vector2(0,0); 
	private float nextAttack=0;
	Vector2 temp;
	Animator anim;
	SpriteRenderer sp;
	DamageScript dm;
	DataKeeper recordingScript ;

	void Start () {
		dm = GameObject.FindWithTag ("Player").GetComponent<DamageScript> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		sp = GetComponent<SpriteRenderer> ();
		InvokeRepeating ("PlatformMovement", 0, ossilationTime);
	}
		
	void Update () {
		rb.velocity=(speed*Time.deltaTime);
	}

	void PlatformMovement(){
		StartCoroutine (SpeedUpdater ());
	}

	IEnumerator SpeedUpdater(){
		temp = speed;
		speed = zeroSpeed;
		anim.SetTrigger("Stay");
		yield return new WaitForSeconds(waitTime);
		speed = -temp;
		anim.SetTrigger("Walk");
		if (speed.y < 0) {
			sp.flipY=true;
			upLight.SetActive (false);
			downLight.SetActive (true);
		}
		else
		{
			sp.flipY=false;
			upLight.SetActive (true);
			downLight.SetActive (false);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			if (Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				dm.TakeDamage (damage);
			}
		} 
		if (other.gameObject.tag == "weapon") {
			anim.SetTrigger ("Dead");
			recordingScript.EnemyKilledIncreasor (enemyType);
			Destroy (this.gameObject,5);
		} 
	}
	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			if (Time.time > nextAttack) {
				nextAttack = Time.time + attackRate;
				dm.TakeDamage (damage);
			}
		}
	} 
}
