using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour {

	[HideInInspector]
	public int damageInflected;

	CircleCollider2D cd;
	ZombieBehav zombieScript;
	SkeletonBehav skeletonScript;
	SpiderBehav spiderScript;

	void Start () {
		cd = GetComponent<CircleCollider2D> ();
		cd.radius = PlayerPrefs.GetFloat ("4");
		StartCoroutine (ExplosionForceStopper ());
	}

	IEnumerator ExplosionForceStopper(){
		yield return new WaitForSeconds(0.1f);
		cd.enabled = false ;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "enemy") {
			zombieScript = other.gameObject.GetComponent<ZombieBehav> ();
			skeletonScript = other.gameObject.GetComponent<SkeletonBehav> ();
			spiderScript = other.gameObject.GetComponent<SpiderBehav> ();
			if (zombieScript != null) {
				zombieScript.TakeDamage (damageInflected);
			}
			if (skeletonScript != null) {
				skeletonScript.TakeDamage (damageInflected);
			}
			if (spiderScript != null) {
				spiderScript.TakeDamage (damageInflected);
			}
		}
	}
}
