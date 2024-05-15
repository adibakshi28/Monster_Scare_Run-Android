using UnityEngine;
using System.Collections;

public class GroundPlayerTrigger : MonoBehaviour {

	[HideInInspector]
	public bool IsGrounded;

	EdgeCollider2D edc;

	void Start(){
		edc=GetComponent<EdgeCollider2D>();
	}

	void OnTriggerStay2D (Collider2D other){

/*		if ((other.gameObject.tag == "enemy") || (other.gameObject.tag == "collectable")) {
			Physics2D.IgnoreCollision (other.GetComponent<Collider2D> (), edc);
		}  */
		if (other.gameObject.tag == "collectable") {
			Physics2D.IgnoreCollision (other.GetComponent<Collider2D> (), edc);
		} else {
			IsGrounded = true;
		}
	}

	void OnTriggerExit2D (Collider2D collisionInfo){
		StartCoroutine (GroundedWaiter ());
	}

	IEnumerator GroundedWaiter(){
		yield return new WaitForSeconds (0.1f);
		IsGrounded = false;
	}
}
