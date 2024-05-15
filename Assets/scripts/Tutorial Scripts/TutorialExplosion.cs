using UnityEngine;
using System.Collections;

public class TutorialExplosion : MonoBehaviour {


	public int damageInflected=100;

	CircleCollider2D cd;
	TutorialZombie zombieScript;
	TutorialSkeleton skeletonScript;

	void Start () {
		cd = GetComponent<CircleCollider2D> ();
		StartCoroutine (ExplosionForceStopper ());
	}

	IEnumerator ExplosionForceStopper(){
		yield return new WaitForSeconds(0.1f);
		cd.enabled = false ;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "enemy") {
			zombieScript = other.gameObject.GetComponent<TutorialZombie> ();
			skeletonScript = other.gameObject.GetComponent<TutorialSkeleton> ();
			if (zombieScript != null) {
				zombieScript.Die ();
			}
			if (skeletonScript != null) {
				skeletonScript.Die ();
			}
		}
	}
}
