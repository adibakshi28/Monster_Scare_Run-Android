using UnityEngine;
using System.Collections;

public class LaserBehavour : MonoBehaviour {

	public int damageInflected=1;
	public float attackRate=0.3f;

	float maxLaserLength=50;
//	int laserDirection;
	Vector3 temp;
	float tempDistance;
	AudioSource aud;
	EdgeCollider2D ed;
	ZombieBehav zombieScript;
	SkeletonBehav skeletonScript;
	SpiderBehav spiderScript;
	DataKeeper recordingScript;
//	RobotController playerScript ;


	void Start () {
//		playerScript = GameObject.FindWithTag ("Player").GetComponent<RobotController> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
//		laserDirection = playerScript.bulletDirection;                    //  checked for direction
		ed=GetComponent<EdgeCollider2D>();
/*		if(laserDirection<0)
		{	
			maxLaserLength = -maxLaserLength;
		}                                         */
		recordingScript.weaponUsed = true;
		transform.localScale = temp;
		temp.x = maxLaserLength;
		transform.localScale = temp;

		InvokeRepeating("Remover",0.3f,attackRate);
	}

	void Update(){

		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);             // only works for shooting to right
		if(hit.collider!=null){
			float tempDistance = Mathf.Abs(hit.point.x - transform.position.x);
			transform.localScale = temp;
			temp.x = tempDistance;
			transform.localScale = temp;
		}
	}

	void Remover(){
		StartCoroutine (Ossilator ());
	}

	IEnumerator Ossilator(){
		ed.enabled = true;
		yield return new WaitForSeconds(0.01f);
		ed.enabled = false;
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

