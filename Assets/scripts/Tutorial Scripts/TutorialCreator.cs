using UnityEngine;
using System.Collections;

public class TutorialCreator : MonoBehaviour {

	public GameObject[]  tutorialGroundTypes;

	private bool created=false;

	TutorialDataController tutorialDataScript;

	void Start(){
		tutorialDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<TutorialDataController> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((other.gameObject.tag == "creationTrigger")&&(!created)) {
			Instantiate (tutorialGroundTypes[tutorialDataScript.platformToGenerate], transform.position, Quaternion.identity);
			created = true;
			StartCoroutine (Destroyer ());
		} 
	}        

	IEnumerator Destroyer(){
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject.transform.parent.gameObject);
	}
}      
