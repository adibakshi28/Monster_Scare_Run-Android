using UnityEngine;
using System.Collections;

public class Enemy_spawner_trigger : MonoBehaviour {

	public GameObject enemy;
	public bool zombieRandomizer=false;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;
	public GameObject enemy6;   

	Vector3 spawningPosition;
	int typeNo;

	void Start(){
		
		spawningPosition = this.gameObject.transform.GetChild (0).position;
		spawningPosition.z = 0.1f;

		if(zombieRandomizer){
				
			typeNo = Random.Range (1, 7);

			switch (typeNo) {
			case 1:	
				enemy = enemy1;
				break;
			case 2:	
				enemy = enemy2;
				break;
			case 3:	
				enemy = enemy3;
				break;
			case 4:	
				enemy = enemy4;
				break;
			case 5:	
				enemy = enemy5;
				break;
			case 6:	
				enemy = enemy6;
				break;
			default:
				Debug.Log(" Default Statement in enemy_spanner script in executed");
				enemy = null;
				break;
			}    
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Instantiate(enemy,spawningPosition,Quaternion.identity);	
			Destroy (this.gameObject);
		}
	}
}

