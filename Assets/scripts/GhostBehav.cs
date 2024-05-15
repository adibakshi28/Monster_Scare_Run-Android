using UnityEngine;
using System.Collections;

public class GhostBehav : MonoBehaviour {

	public float movementSpeed=8; 
	public float ossilationSpeed=2;
	public float ossilationTime=0.1f;
	public int damage=2;

	private int enemyType=4;
	DamageScript dm;
	DataKeeper recordingScript ;

	void Start () {
		dm = GameObject.FindWithTag ("Player").GetComponent<DamageScript> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		Destroy (this.gameObject, 10);
		InvokeRepeating ("Ossilate", 0, ossilationTime);
	}
	

	void Update () {
	//	transform.Translate (0, risingSpeed * Time.deltaTime, 0);
		transform.Translate (movementSpeed * Time.deltaTime,0, 0);
		transform.Translate (0,ossilationSpeed * Time.deltaTime,0);
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
				dm.TakeDamage (damage);
		} 
		if (other.gameObject.tag == "weapon") {
			recordingScript.EnemyKilledIncreasor (enemyType);
			Destroy (this.gameObject);
		} 
	}
    
	void Ossilate(){
		ossilationSpeed = -ossilationSpeed;
	}
}
