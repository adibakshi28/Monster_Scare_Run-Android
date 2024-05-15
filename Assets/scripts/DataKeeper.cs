using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataKeeper : MonoBehaviour {

	public Text targetText;
	public Text targetCount;
//	public AudioClip increaseAudio;                   //  play this clip when aim is like passin by pumpkin or tombstones (no audio source items in aim)

	//	[HideInInspector]
	public float distanceTravelled=0;
	//	[HideInInspector]
	public int enemiesKilled=0;
	//	[HideInInspector]
	public int coinsCollected=0;
	//	[HideInInspector]
	public int powerupsCollected=0;
	//	[HideInInspector]
	public int pumpkinPassedBy=0;
	//	[HideInInspector]
	public int tombstonesPassedBy=0;
	// [HideInInspector]
	public int zombieKilled=0;
	// [HideInInspector]
	public int skeletonKilled=0;
	// [HideInInspector]
	public int spiderKilled=0;
	// [HideInInspector]
	public int ghostKilled=0;
	// [HideInInspector]
	public float distanceTravelledWithoutUsingWeapon=0;
	// [HideInInspector]
	public float distanceTravelledWithoutCollectingCoin=0;

	[HideInInspector]
	public bool weaponUsed=false,coinCollected=false;

	private int gameType,target,currentLevel,totalLevelsInGame;

//	AudioSource aud;
	GameDataController gameControllerScript;
	LevelDataController levelControllerScript;

	void Start () {
		gameControllerScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();
		levelControllerScript = GetComponent<LevelDataController> ();
//		aud = GetComponent<AudioSource> ();
		gameType = gameControllerScript.gameType;
		target = gameControllerScript.target;
		currentLevel = gameControllerScript.currentLevel;
		totalLevelsInGame = gameControllerScript.totalLevelsInGame;

		switch(gameType){
		case 1:
			targetText.text = "DISTANCE";
			break;
		case 2:
			targetText.text = "COINS";
			break;
		case 3:
			targetText.text = "ENEMIES";
			break;
		case 4:
			targetText.text = "POWERUPS";
			break;
		case 5:
			targetText.text = "JACK-o_LANTERNS";
			break;
		case 6:
			targetText.text = "TOMBSTONES";
			break;
		case 7:
			targetText.text = "ZOMBIES";
			break;
		case 8:
			targetText.text = "SKELETONS";
			break;
		case 9:
			targetText.text = "SPIDERS";
			break;
		case 10:
			targetText.text = "GHOSTS";
			break;
		case 11:
			targetText.text = "DISTANCE";
			break;
		case 12:
			targetText.text = "DISTANCE";
			break;
		default:
			targetText.text = "UNKNOWN";
			break;
		}
	}

	public void DistanceUpdater(int distance){
		distanceTravelled = distance;
	                	// Total distance travelled player pref is updated when game ends ie in HighScreSetter()
		if(!(weaponUsed)){
			distanceTravelledWithoutUsingWeapon = distance;
		}
		if(!(coinCollected)){
			distanceTravelledWithoutCollectingCoin = distance;
		}
		DataShow ();
	}

	public void EnemyKilledIncreasor(int type){
		enemiesKilled++;
		PlayerPrefs.SetInt ("totalEnemiesKilled", PlayerPrefs.GetInt ("totalEnemiesKilled") + 1);
		switch (type) {
		case 1: 
			zombieKilled++;
			break;
		case 2: 
			skeletonKilled++;
			break;
		case 3: 
			spiderKilled++;
			break;
		case 4: 
			ghostKilled++;
			break;
		}
		DataShow ();
	}
	public void CoinIncreasor(){
		coinsCollected++;	
		PlayerPrefs.SetInt ("totalCoinsCollected", PlayerPrefs.GetInt ("totalCoinsCollected") + 1);
		DataShow ();
	}
	public void PowerupsIncreasor(){
		powerupsCollected++;
		PlayerPrefs.SetInt ("totalPowerupsCollected", PlayerPrefs.GetInt ("totalPowerupsCollected") + 1);
		DataShow ();
	}
	public void PumpkinPassedIncreasor(){
		pumpkinPassedBy++;
		PlayerPrefs.SetInt ("totalPumpkinPassedBy", PlayerPrefs.GetInt ("totalPumpkinPassedBy") + 1);
/*		aud.clip = increaseAudio;
		aud.Play ();            */
		DataShow ();
	}
	public void TombstonesPassedIncreasor(){
		tombstonesPassedBy++;
		PlayerPrefs.SetInt ("totalTombstonesPassedBy", PlayerPrefs.GetInt ("totalTombstonesPassedBy") + 1);
/*		aud.clip = increaseAudio;
		aud.Play ();           */
		DataShow ();
	}

	 void DataShow(){
		switch(gameType){
		case 1:
			targetCount.text = distanceTravelled.ToString (); 
			if (distanceTravelled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 2:
			targetCount.text = coinsCollected.ToString (); 
			if (coinsCollected == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 3:
			targetCount.text = enemiesKilled.ToString ();
			if (enemiesKilled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 4:
			targetCount.text = powerupsCollected.ToString (); 
			if (powerupsCollected == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 5:
			targetCount.text = pumpkinPassedBy.ToString (); 
			if (pumpkinPassedBy == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 6:
			targetCount.text = tombstonesPassedBy.ToString (); 
			if (tombstonesPassedBy == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 7:
			targetCount.text = zombieKilled.ToString (); 
			if (zombieKilled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 8:
			targetCount.text = skeletonKilled.ToString (); 
			if (skeletonKilled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 9:
			targetCount.text = spiderKilled.ToString (); 
			if (spiderKilled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 10:
			targetCount.text = ghostKilled.ToString (); 
			if (ghostKilled == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		case 11:
			targetCount.text = distanceTravelledWithoutUsingWeapon.ToString (); 
			if (distanceTravelledWithoutUsingWeapon == target) {
				levelControllerScript.targetAchieved = true;
			}
			break;
		default:
			break;
		}
	}

	public void HighScoreSetter(){
		PlayerPrefs.SetInt ("totalDistanceTravelled", PlayerPrefs.GetInt ("totalDistanceTravelled") + (int)distanceTravelled);  // to update total distance data 
		for (int lvl = 0; lvl <= totalLevelsInGame; lvl++) {
			if (lvl == currentLevel) {
				switch (gameType) {
				case 1:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < distanceTravelled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", (int)distanceTravelled);
					}
					break;
				case 2:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < coinsCollected) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", coinsCollected);
					}
					break;
				case 3:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < enemiesKilled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", enemiesKilled);
					}
					break;
				case 4:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < powerupsCollected) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", powerupsCollected);
					}
					break;
				case 5:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < pumpkinPassedBy) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", pumpkinPassedBy);
					}
					break;
				case 6:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < tombstonesPassedBy) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", tombstonesPassedBy);
					}
					break;
				case 7:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < zombieKilled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", zombieKilled);
					}
					break;
				case 8:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < skeletonKilled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", skeletonKilled);
					}
					break;
				case 9:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < spiderKilled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", spiderKilled);
					}
					break;
				case 10:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < ghostKilled) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", ghostKilled);
					}
					break;
				case 11:
					if (PlayerPrefs.GetInt ("Level " + lvl.ToString () + " Highscore") < distanceTravelledWithoutUsingWeapon) {
						PlayerPrefs.SetInt ("Level " + lvl.ToString () + " Highscore", (int)distanceTravelledWithoutUsingWeapon);
					}
					break;
				default:
					break;
				}
			}
		}
	}
}
