using UnityEngine;
using System.Collections;

public class BackgroundMusicController : MonoBehaviour {

	public AudioClip primaryBackgroundMusic;
	public AudioClip wolvesBaring;
	public AudioClip monsterSound;
	public AudioClip diabolicalLaugh;
	public AudioClip owlSound;
	public GameObject bkSoundEffects;
	public float startTime=5;
	public int repeatTime=15;

	AudioSource aud;

	void Start () {
		aud = bkSoundEffects.GetComponent<AudioSource> ();
		InvokeRepeating("EffectPlayer",startTime,repeatTime);
	}

	void EffectPlayer(){
		int caseNo = Random.Range (1,5);
		switch (caseNo) {
		case 1:
			aud.clip = wolvesBaring;
			aud.Play ();
			break;
		case 2:
			aud.clip = monsterSound;
			aud.Play ();
			break;
		case 3:
			aud.clip = diabolicalLaugh;
			aud.Play ();
			break;
		case 4:
			aud.clip = owlSound;
			aud.Play ();
			break;
		default:
			aud.clip = null;
			break;
		}
	}
}
