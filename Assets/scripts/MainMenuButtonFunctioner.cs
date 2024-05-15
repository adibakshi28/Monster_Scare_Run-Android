using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctioner : MonoBehaviour {
	
	public AudioClip chainRattlingAudio, selectAudio;
	public GameObject canvas;

	private bool exitMenuActive=false;

	Animator anim;
	AudioSource aud;

	void Start(){
		aud = GetComponent<AudioSource> ();
		anim = canvas.GetComponent<Animator> ();
	}

	void Update(){
		if ((Input.GetKeyDown (KeyCode.Escape))&&(!exitMenuActive)) {
			exitMenuActive = true;
			aud.clip = chainRattlingAudio;
			aud.PlayDelayed (0.15f);
			anim.SetTrigger ("Enter");
		}
	}

	public void NewGame(){
		if (PlayerPrefs.GetInt ("firstTime") == 1) {
			StartCoroutine (Waiter ("Story Scene"));
		} 
		else { 
			StartCoroutine (Waiter ("Level Select"));
		}
	}

	public void Exit(){
		if ((!exitMenuActive)) {
			exitMenuActive = true;
			aud.clip = chainRattlingAudio;
			aud.PlayDelayed (0.15f);
			anim.SetTrigger ("Enter");
		}
	}

	IEnumerator Waiter(string sceneToLoad){
		aud.clip = selectAudio;
		aud.Play ();
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene (sceneToLoad);
	}

	public void ExitTrueButton(){
		Application.Quit ();
		return;
	}
	public void ExitCancelButton(){
		anim.SetTrigger ("Exit");
		aud.clip = chainRattlingAudio;
		aud.PlayDelayed (0.4f);
		exitMenuActive = false;
	}
}
