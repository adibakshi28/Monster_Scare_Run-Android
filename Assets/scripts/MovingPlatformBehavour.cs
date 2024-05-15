using UnityEngine;
using System.Collections;

public class MovingPlatformBehavour : MonoBehaviour {

	public float ossilationTime;
	public Vector2 speed;

	Rigidbody2D rb;
	Vector2 zeroSpeed=new Vector2(0,0); 
	Vector2 temp;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("PlatformMovement", 0, ossilationTime);
	}
	

	void Update () {
	//	transform.Translate (speed*Time.deltaTime);
		rb.velocity=(speed*Time.deltaTime);

	}

	void PlatformMovement(){
		StartCoroutine (SpeedUpdater ());
	}

	IEnumerator SpeedUpdater(){
		temp = speed;
		speed = zeroSpeed;
		yield return new WaitForSeconds(0.3f);
		speed = -temp;;
	}
}
