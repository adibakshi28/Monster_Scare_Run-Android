using UnityEngine;
using System.Collections;

public class DemoMissile : MonoBehaviour {

	public float rotationSpeed=10;
//	public GameObject explosion; 

//	GameObject child1,child2,createdExplosion;

	void Start () {
//		child1 = this.gameObject.transform.GetChild (0).gameObject;
//		child2 = this.gameObject.transform.GetChild (1).gameObject;
//		InvokeRepeating ("Explode", 5, 8);
	}

	void Update () {
		transform.Rotate (rotationSpeed, 0, 0);
	}

/*	void Explode(){
		child1.SetActive (false);
		child2.SetActive (false);
		createdExplosion = Instantiate (explosion, transform.position, Quaternion.identity)as GameObject;
		StartCoroutine (Creator ());
	}
	IEnumerator Creator(){
		yield return new WaitForSeconds(4);
		Destroy (createdExplosion);
		child1.SetActive (true);
		child2.SetActive (true);
	}		*/
}
