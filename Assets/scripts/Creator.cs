using UnityEngine;
using System.Collections;

public class Creator : MonoBehaviour {

	public GameObject[] groundTypes1;
//	public GameObject[] groundTypes2;
//	public GameObject[] groundTypes3;
//	public GameObject[] groundTypes4;
//	public GameObject[] groundTypes5;
	public int currentTypeNo;

	private bool created=false;
	private GameObject platform;
	int typeNo;
//	int platformGeneratorType;

	LevelDataController levelDataScript;

	void Start(){

		levelDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<LevelDataController> ();

//		platformGeneratorType = levelDataScript.platformGeneratorType;
	
		do {
			typeNo = Random.Range (0, groundTypes1.GetLength (0));
		} while((typeNo == currentTypeNo)||(typeNo==0));

	/*	switch (platformGeneratorType) {
		case 1:
			platform = groundTypes1 [typeNo];
			break;
		case 2:
			platform = groundTypes2 [typeNo];
			break;
		case 3:
			platform = groundTypes3 [typeNo];
			break;
		case 4:
			platform = groundTypes4 [typeNo];
			break;
		case 5:
			platform = groundTypes5 [typeNo];
			break;    
		default:                                                 // default statement is only for editor purpose else it has no use
			Debug.Log(" Default Statement in creator script in executed,, value of platformGeneratorType="+platformGeneratorType);
			platform = groundTypes1 [typeNo];
			break;
		}  */

		platform = groundTypes1 [typeNo];

	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((other.gameObject.tag == "creationTrigger")&&(!created)) {
			Instantiate (platform, transform.position, Quaternion.identity);
			created = true;
			StartCoroutine (Destroyer ());
		} 
	}        

	IEnumerator Destroyer(){
		yield return new WaitForSeconds (10);
		if (!(levelDataScript.dead)) {
			Destroy (this.gameObject.transform.parent.gameObject);
		}
	}
}      
