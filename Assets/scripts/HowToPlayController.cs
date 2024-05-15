using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour {

	public AudioClip mainMenuClickAudio;

	AudioSource aud;

	void Start(){
		aud = GetComponent<AudioSource> ();
	}

	public void MainMenuButton(){
		aud.clip = mainMenuClickAudio;
		StartCoroutine (Waiter ("Main Menu"));
	}

	IEnumerator Waiter(string sceneToLoad){
		aud.Play ();
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene (sceneToLoad);
	}
}
