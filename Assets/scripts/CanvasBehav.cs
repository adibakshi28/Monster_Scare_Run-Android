using UnityEngine;
using System.Collections;

public class CanvasBehav : MonoBehaviour {

	public AudioClip heartBeat;
	public AudioClip gameOverAudio;
//	public AudioClip gameWonAudio;

	Animator anim;
	AudioSource aud;

	void Start () {
		anim = GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();
	}

	public void CoinIncrease(){
		anim.SetTrigger ("Coins");
	}

	public void GameWonAnimation(){
/*		aud.clip = gameWonAudio;
		aud.loop = false;
		aud.Play ();*/
		anim.SetTrigger ("GameWon");
	}

	public void GameOverAnimation(){
		aud.clip = gameOverAudio;
		aud.loop = false;
		aud.Play ();
		anim.SetTrigger ("GameOver");
	}

	public void PauseAnimation(){
		anim.SetBool ("Pause", true);
	}

	public void UnpauseAnimation(){
		anim.SetBool ("Pause", false);
	}

	public void BlinkingHeartAnimation(){
		aud.clip = heartBeat;
		aud.loop = true;
		aud.Play ();
		anim.SetBool ("HeartBlinker", true);
	}

	public void BlinkingHeartStopAnimation(){
		aud.clip = null;
		aud.Pause ();
		anim.SetBool ("HeartBlinker", false);
	}
}
