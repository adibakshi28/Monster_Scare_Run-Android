using UnityEngine;
using System.Collections;

public class RocketPowerUp : MonoBehaviour {
	
	public Vector2 velocity=new Vector2(4.1f,-6);
	public float ossilationTime = 0.5f;
//	public int missilesIncreased=5;
	public int destroyTime=30;
	public GameObject childObject;

	LevelDataController levelDataScript;
	DataKeeper recordingScript ;
	AudioSource aud;
	Rigidbody2D rb;
	CircleCollider2D cd;
	BoxCollider2D bx;

	void Start () {
		aud = GetComponent<AudioSource> ();
		levelDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<LevelDataController> ();
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
			levelDataScript.missilesInLevel = levelDataScript.missilesInLevel + (int)(PlayerPrefs.GetFloat ("6"));
			levelDataScript.missileCountText.text = levelDataScript.missilesInLevel.ToString ();
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
			levelDataScript.missilesInLevel = levelDataScript.missilesInLevel + (int)(PlayerPrefs.GetFloat ("6"));
			levelDataScript.missileCountText.text = levelDataScript.missilesInLevel.ToString ();
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
