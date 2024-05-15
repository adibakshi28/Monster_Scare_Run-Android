using UnityEngine;
using System.Collections;

public class TriggerCollector : MonoBehaviour {

	public bool pumpkinChecker=false;
	public bool tombstoneChecker=false;

	DataKeeper recordingScript ;

	void Start () {
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			RecordUpdater ();
		} 
	}

	void RecordUpdater(){
		if (pumpkinChecker) {
			recordingScript.PumpkinPassedIncreasor ();
		}
		if (tombstoneChecker) {
			recordingScript.TombstonesPassedIncreasor ();
		}
	}
}
