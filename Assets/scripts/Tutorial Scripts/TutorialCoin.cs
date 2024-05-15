using UnityEngine;
using System.Collections;

public class TutorialCoin : MonoBehaviour {

	AudioSource coinAudio;
	Animator anim;

	TutorialDataController tutorialDataScript;

	void Start () {
		tutorialDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<TutorialDataController> ();
		coinAudio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			tutorialDataScript.CoinIncreasor(1);
			coinAudio.Play ();
			anim.SetTrigger ("Collected");
			Destroy (this.gameObject,0.5f);
		} 
	}
}
