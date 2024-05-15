using UnityEngine;
using System.Collections;

public class SpeedSlowPowerUp : MonoBehaviour {

	public Vector2 velocity=new Vector2(5f,-15);
	public float ossilationTime = 1f;
//	public float speedSlowFactor=0.8f;
//	public int duration=15;
	public int destroyTime=30;
	public GameObject childObject;

//	RobotController playerScript;
	PlayerScript playerScript;
	DataKeeper recordingScript ;
	AudioSource aud;
	Rigidbody2D rb;
	CircleCollider2D cd;
	BoxCollider2D bx;

	void Start () {
		aud = GetComponent<AudioSource> ();
		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		rb = GetComponent<Rigidbody2D> ();
		cd = GetComponent<CircleCollider2D> ();
		bx = GetComponent<BoxCollider2D> ();
		Destroy (this.gameObject,destroyTime);
	}


	void FixedUpdate () {
		rb.velocity=velocity;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			cd.enabled = false;
			bx.enabled = false;
			aud.Play ();
			Destroy (childObject);
			Destroy (this.gameObject, 0.5f);
			playerScript.SpeedSlowerPowerup ((int)(PlayerPrefs.GetFloat("8")),0.85f);
			recordingScript.PowerupsIncreasor ();
		}
		else{
			velocity.x=-velocity.x;
		}
	}	   

	void OnCollisionEnter2D(Collision2D other){
		if ((other.gameObject.tag == "death")) {
			Destroy (this.gameObject);
		} 
		if (other.gameObject.tag == "Player") {
			cd.enabled = false;
			bx.enabled = false;
			aud.Play ();
			Destroy (childObject);
			Destroy (this.gameObject, 0.5f);
			playerScript.SpeedSlowerPowerup ((int)(PlayerPrefs.GetFloat("8")),0.85f);
			recordingScript.PowerupsIncreasor ();
		}
		else {
			velocity.y = -velocity.y;
			StartCoroutine (velocityChangeWait ());
		}
	}


	IEnumerator velocityChangeWait(){
		yield return new WaitForSeconds(ossilationTime);
		velocity.y = -velocity.y;
	} 

}
