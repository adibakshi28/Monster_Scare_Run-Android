using UnityEngine;
using System.Collections;

public class TutorialMissile : MonoBehaviour {

	public Vector2 velocity=new Vector2(10,0);
	public float rotationSpeed=10;
	public GameObject explosion; 

	Rigidbody2D rb;

	void Start () {

		rb = GetComponent<Rigidbody2D> ();
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
