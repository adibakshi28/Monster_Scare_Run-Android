using UnityEngine;
using System.Collections;

public class FlameThrowerController : MonoBehaviour {

	public int damageInflected;
	public float attackRate=0.3f;
//	public GameObject fire;

	CircleCollider2D cd;
	BoxCollider2D bx;
	ZombieBehav zombieScript;
	SkeletonBehav skeletonScript;
	SpiderBehav spiderScript;
	DataKeeper recordingScript;

	void Start(){
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		bx = GetComponent<BoxCollider2D> ();
		cd = GetComponent<CircleCollider2D> ();
		recordingScript.weaponUsed = true;
		bx.enabled = false;
		cd.enabled = false;
		InvokeRepeating("Remover",0.3f,attackRate);
	}

	void Remover(){
		StartCoroutine (Ossilator ());
	}

	IEnumerator Ossilator(){
		bx.enabled = true;
		cd.enabled = true;
		yield return new WaitForSeconds(0.01f);
		bx.enabled = false;
		cd.enabled = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{ 
		if ( other.gameObject.tag == "enemy") {
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

