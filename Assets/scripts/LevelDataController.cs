using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDataController : MonoBehaviour {

	public GameObject mainLevelCanvas;
	public GameObject bkMusicController;
	public GameObject mainCamera;
	public AudioListener gameAudioListner;
	public AudioClip pauseAudio;
	public AudioClip unPauseAudio;
	public AudioClip mainMenuAudio;
	public Text coinText;
	public Text missileCountText;
	public Text aimText;
	public Slider playerHealthSlider;
	public Image playerDamageImage;
	public Text playerHealthText;
	public int missilesInLevel=10;
	public int distanceMultiplier = 10;
	public Text distanceText;
	public GameObject[] players;
	[HideInInspector]
	public int coins;
	[HideInInspector]
	public bool targetAchieved = false;
	[HideInInspector]
	public bool dead=false;

	[HideInInspector]                                      
	public float playerAcclerationRate;
	[HideInInspector]
	public int platformGeneratorType;
	[HideInInspector]
	public float enemyHealthMultiplier;
	[HideInInspector]
	public float enemyDamageMultiplier;

	private Vector3 playerStartingPosition = new Vector3 (6,5,0);
	private int currentLevel;
	private float distanceTravelled;
	private GameObject player;
	GameDataController gameControllerScript;
	DataKeeper recordingScript ;
	CanvasBehav canavsScript;
	CameraController cameraControllingScript;
	PlayerScript playerScript;
	AudioSource aud;

	void Awake(){
		
		switch(PlayerPrefs.GetInt ("activePlayer")){
		case 1:
			player = Instantiate (players [0], playerStartingPosition, Quaternion.identity)as GameObject;      // Creates player in level
			break;
		}

		cameraControllingScript = mainCamera.GetComponent<CameraController> ();
		cameraControllingScript.enabled = true;  // enables camera script ( it reffers to player so it should be done after creating player
		playerScript=player.GetComponent<PlayerScript>();
	}

	void Start () {
		gameControllerScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();
		canavsScript = mainLevelCanvas.GetComponent<CanvasBehav> ();
		recordingScript = GetComponent<DataKeeper> ();
		aud = GetComponent<AudioSource> ();

		coins = PlayerPrefs.GetInt("coins");
		missilesInLevel = (int)(PlayerPrefs.GetFloat ("3"));

		currentLevel = gameControllerScript.currentLevel;
		playerAcclerationRate = gameControllerScript.playerAcclerationRate;
		platformGeneratorType = gameControllerScript.platformGeneratorType;
		enemyHealthMultiplier = gameControllerScript.enemyHealthMultiplier;
		enemyDamageMultiplier = gameControllerScript.enemyDamageMultiplier;
		aimText.text = gameControllerScript.aimText;

		coinText.text = coins.ToString (); 
		missileCountText.text = missilesInLevel.ToString ();
	}

	void Update () { 
		if (!dead) {
			distanceTravelled += Time.deltaTime;
			distanceText.text = (Mathf.Round (distanceTravelled * distanceMultiplier)).ToString ();	
			recordingScript.DistanceUpdater ((int) Mathf.Round (distanceTravelled * distanceMultiplier));
		}
	}

	public void HeartBlinker (){ 
		canavsScript.BlinkingHeartAnimation ();
	}

	public void HeartBlinkerStopper (){ 
		canavsScript.BlinkingHeartStopAnimation ();
	}
		
	public void CoinIncreasor (int number){
		coins = coins + number;
		PlayerPrefs.SetInt ("coins", coins);
		canavsScript.CoinIncrease ();
		coinText.text = coins.ToString (); 
	}
		
	public void GameEnd(){
		dead = true;
		recordingScript.HighScoreSetter ();
		if (targetAchieved) {
			StartCoroutine (Won ());
		} 
		else {
		    StartCoroutine (Lost ());
		}
	}
	IEnumerator Won(){
		Destroy (bkMusicController);
		if(PlayerPrefs.GetInt ("levelReached")==currentLevel){
		  PlayerPrefs.SetInt ("levelReached", currentLevel+1);  
		}
		yield return new WaitForSeconds(0.4f);
		canavsScript.GameWonAnimation ();
		gameControllerScript.showInterstitialAd ();            //  show interinstial ads
	}
	IEnumerator Lost(){
		Destroy (bkMusicController); 
		yield return new WaitForSeconds(0.4f);
		canavsScript.GameOverAnimation ();
		gameControllerScript.showInterstitialAd ();            //  show interinstial ads
	}

	public void Pause(){
		aud.clip = pauseAudio;
		aud.Play ();
		canavsScript.PauseAnimation ();
		StartCoroutine (PauseWaiter ());
	}
	IEnumerator PauseWaiter(){
		yield return new WaitForSeconds(0.1f);
		gameAudioListner.enabled = false;
		Time.timeScale = 0;	
	}

	public void UnPause(){
		Time.timeScale = 1;
		canavsScript.UnpauseAnimation ();
		gameAudioListner.enabled = true;
		aud.clip = unPauseAudio;
		aud.Play ();
	}
		
	public void LevelSelection(){
		Time.timeScale = 1;
		aud.clip = mainMenuAudio;
		aud.Play ();
		SimpleSceneFader.ChangeSceneWithFade("Level Select");
	}

	public void Restart(){
		gameControllerScript.RestartLevel ();
	}

	public void FireButton(){
		playerScript.AndFire ();
	}

}     