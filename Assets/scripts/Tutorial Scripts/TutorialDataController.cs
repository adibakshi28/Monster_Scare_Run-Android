using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDataController : MonoBehaviour {

	public GameObject mainCamera;
	public AudioListener gameAudioListner;
	public Text coinText;
	public Text missileCountText;
	public Slider playerHealthSlider;
	public Image playerDamageImage;
	public Text playerHealthText;
	public int missilesInTutorial=10;
	public int distanceMultiplier = 10;
	public Text distanceText;
	public Text displayText;
	public GameObject demoPlayer;
	public GameObject swipeUpImage, swipeDownImage, healthArrow, coinArrow, fireButtonArrow, distanceArrow, ammoArrow;
	[HideInInspector]
	public int coins;
	[HideInInspector]
	public int platformToGenerate = 0;

	private bool next = false;
	private int sequence, seq = 1;
	private Vector3 playerStartingPosition = new Vector3 (6,5,0);
	private float distanceTravelled;
	AudioSource aud;
	private GameObject player;
	CanvasBehav canavsScript;
	CameraController cameraControllingScript;
	TutorialPlayerScript playerScript;

	void Awake(){
		player = Instantiate (demoPlayer, playerStartingPosition, Quaternion.identity)as GameObject;      // Creates player in tutorial
		cameraControllingScript = mainCamera.GetComponent<CameraController> ();
		cameraControllingScript.enabled = true;  // enables camera script ( it reffers to player so it should be done after creating player
		playerScript=player.GetComponent<TutorialPlayerScript>();
	}

	void Start () {
		aud = GetComponent<AudioSource> ();
		coins = PlayerPrefs.GetInt ("coins");

		coinText.text = coins.ToString (); 
		missileCountText.text = missilesInTutorial.ToString ();
		StartCoroutine (TextWriter1 ("WELCOME   !",2,1,"SwipeUp"));
	}

	void Update () { 
		distanceTravelled += Time.deltaTime;
		distanceText.text = (Mathf.Round (distanceTravelled * distanceMultiplier).ToString ());
		if ((next) && (Input.touchCount!=0)) {
			switch(seq){
			case 1: 
				StartCoroutine (TextWriter3 ("IS A MEASURE OF DISTANCE TRAVELLED BY PLAYER", 0, distanceArrow, healthArrow));
				break;
			case 2:
				StartCoroutine (TextWriter3 ("DISPLAYS AMMO IN THE WEAPON", 0, ammoArrow, distanceArrow));	
				break;
			case 3:
				StartCoroutine (TextWriter3 ("DISPLAYS THE NUMBER OF COINS COLLECTED BY THE PLAYER", 2,coinArrow ,ammoArrow));	
				break;
			case 4:
				StartCoroutine (TextWriter3 ("COINS ARE REQUIRED TO UPGRADE PLAYER AND WEAPONS... \n SO COLLECT AS MANY AS POSSIBLE  !", 0,coinArrow,coinArrow));	
				break;
			case 5:
				StartCoroutine (TextWriter3 ("GOOD LUCK  !...", 0,coinArrow,coinArrow,"SkipTutorial",1));	
				break;
			}
			next = false;
			seq++;
		}
	}		    
		
	public void CoinIncreasor (int number){
		coins = coins + number;
		PlayerPrefs.SetInt ("coins", coins);
		coinText.text = coins.ToString (); 
	}
		
	public void FireButton(){
		playerScript.AndFire ();
	}

	IEnumerator TextWriter1(string text,float displayTime,float displayOffsetTime,string functionToCall="none"){
		yield return new WaitForSeconds(displayOffsetTime);
		for (int i = 0; i < text.Length; i++) {
			displayText.text = displayText.text.ToString () + text [i].ToString ();
			yield return new WaitForSeconds(0.13f);
		}
		yield return new WaitForSeconds(displayTime);
		displayText.text = "";
		if (!(functionToCall == "none")) {
			Invoke (functionToCall, 0);
		}
	}


	IEnumerator TextWriter2(string text,float displayOffsetTime,int sequenceFunction=0){
		yield return new WaitForSeconds(displayOffsetTime);
		for (int i = 0; i < text.Length; i++) {
			displayText.text = displayText.text.ToString () + text [i].ToString ();
			yield return new WaitForSeconds(0.07f);
		}
		switch (sequenceFunction) {
		case 1:
			playerScript.SwipeUpStarter ();
			break;
		case 2:
			playerScript.SwipeDownStarter ();
			break;
		case 3:
			playerScript.FireStarter ();
			break;
		}
	}


	public void TextWriter3Starter(){
		StartCoroutine(TextWriter3("SLIDER REPRESENTS CURRENT HEALTH OF PLAYER ",1,healthArrow,fireButtonArrow));
	}
	IEnumerator TextWriter3(string txt,int platformIndiceToGenerate,GameObject toSwitchOn=null,GameObject toSwitchOf=null,string functionToCall="none",float displayTime=0){

		displayText.fontSize = 40;

		displayText.text = "";
		toSwitchOf.SetActive(false);
		toSwitchOn.SetActive (true);
		platformToGenerate = platformIndiceToGenerate;                                              // generate zombie platform
		for (int i = 0; i < txt.Length; i++) {
			displayText.text = displayText.text.ToString () + txt [i].ToString ();
			yield return new WaitForSeconds (0.07f);
		}
		next = true;
		yield return new WaitForSeconds(displayTime);
		if (!(functionToCall == "none")) {
			Invoke (functionToCall, 0);
		}
	}

	void SwipeUp(){
		StartCoroutine (TextWriter2 ("SWIPE UP TO JUMP",0,1));
		swipeUpImage.SetActive (true);
	}

	void SwipeDown(){
		StartCoroutine (TextWriter2 ("SWIPE DOWN TO CROUCH",0,2));
		swipeDownImage.SetActive (true);
	}

	void Shoot(){
		StartCoroutine (TextWriter2 ("PRESS SHOOT TO FIRE WEAPON",0,3));
		fireButtonArrow.SetActive (true);
	}

	public void TaskCompletor(string text_,float displayTime_,float displayOffsetTime_,string functionToCall_="none",int stopSequence=0){
		aud.Play ();
		switch (stopSequence) {
		case 1:
			swipeUpImage.SetActive (false);
			displayText.text = "";
			break;
		case 2:
			swipeDownImage.SetActive (false);
			displayText.text = "";
			break;
		case 3:
			fireButtonArrow.SetActive (false);
			displayText.text = "";
			break;
		default:
			break;
		}
		StartCoroutine (TextWriter1 (text_,displayTime_,displayOffsetTime_,functionToCall_));
	}
		
	public void SkipTutorial(){
		SimpleSceneFader.ChangeSceneWithFade("Level Select");
	}


}     