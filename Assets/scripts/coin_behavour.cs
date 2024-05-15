using UnityEngine;
using System.Collections;

public class coin_behavour : MonoBehaviour {

	AudioSource coinAudio;
	Animator anim;

	LevelDataController levelDataScript;
	DataKeeper recordingScript;

	void Start () {
		levelDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<LevelDataController> ();
		recordingScript = GameObject.FindWithTag ("levelDataController").GetComponent<DataKeeper> ();
		coinAudio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}
		
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			recordingScript.CoinIncreasor ();
			recordingScript.coinCollected = true;
			levelDataScript.CoinIncreasor(1);
			coinAudio.Play ();
			anim.SetTrigger ("Collected");
			Destroy (this.gameObject,0.5f);
		} 
	}
}
