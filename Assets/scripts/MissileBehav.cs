using UnityEngine;
using System.Collections;

public class MissileBehav : MonoBehaviour {
	
	public Vector2 velocity=new Vector2(10,0);
	public float rotationSpeed=10;
	public GameObject explosion; 

	int bulletDirection;
//	RobotController playerScript;
	PlayerScript playerScript;
	DataKeeper recordingScript;
	explosionController explosionScript;
	Rigidbody2D rb;

	void Start () {
//		playerScript = GameObject.FindWithTag ("Player").GetComponent<RobotController> ();
		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		explosionScript = explosion.GetComponent<explosionController> ();
		rb = GetComponent<Rigidbody2D> ();
		recordingScript.weaponUsed = true;
		bulletDirection = playerScript.bulletDirection;
		velocity.x = velocity.x * bulletDirection;
		explosionScript.damageInflected = (int)(PlayerPrefs.GetFloat ("2"));
		if(bulletDirection<0)
		{	
			Quaternion temp = transform.rotation;
			temp.y = 180;
			transform.rotation = temp;
	    }
		Destroy (this.gameObject, 4);
	}
		
	void FixedUpdate () {
		rb.velocity=velocity;
		transform.Rotate (rotationSpeed, 0, 0);
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "collectable") {
			;                        // do nothing
		} else {
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
