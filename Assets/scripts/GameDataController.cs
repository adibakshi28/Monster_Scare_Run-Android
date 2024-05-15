using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
//using admob;

public class GameDataController : MonoBehaviour {

	public int currentVersion;                       // increment this field by 1 in every newer versions of the game release
	public string instiantialID;
	InterstitialAd interstitial;

	[HideInInspector]
	public int gameType;
	[HideInInspector]
	public int target;
	[HideInInspector]
	public string aimText;
	[HideInInspector]
	public int currentLevel;
	[HideInInspector]
	public int totalLevelsInGame;

	[HideInInspector]
	public float playerAcclerationRate;
	[HideInInspector]
	public int platformGeneratorType;
	[HideInInspector]
	public float enemyHealthMultiplier;
	[HideInInspector]
	public float enemyDamageMultiplier;

	int hasPlayed;

	void Start () {
		if (PlayerPrefs.GetInt ("hasPlayed") < 50) {                  // (50 is used to ensure hasPlayed has a value less than 100 whic indicates that the game has been played previously...... expect that 100 and 50 as numbers ha no other significance)

			Debug.Log ("Application Running for the first time");
		
			//Game Settings
			PlayerPrefs.SetInt ("hasPlayed", 100);
			PlayerPrefs.SetInt ("firstTime", 1);              // if playing for the first time the 1 else 0 always
			PlayerPrefs.SetInt("timesLaunched",0);            //  keeps track of times the application is launched
			PlayerPrefs.SetInt ("coins", 0);                       
			PlayerPrefs.SetInt ("levelReached", 1);  
			PlayerPrefs.SetInt ("activePlayer", 1);         // Reffers to default Active Player  , ie 1 ,, Player Changer changes this field and assign Player 
			// Setting Fields to currently active player ( in shop)

			PlayerPrefs.SetInt ("totalDistanceTravelled", 0);
			PlayerPrefs.SetInt ("totalCoinsCollected", 0);
			PlayerPrefs.SetInt ("totalEnemiesKilled", 0);
			PlayerPrefs.SetInt ("totalPowerupsCollected", 0);
			PlayerPrefs.SetInt ("totalPumpkinPassedBy", 0);
			PlayerPrefs.SetInt ("totalTombstonesPassedBy", 0);


			PlayerPrefs.SetFloat ("character 1 health", 100);             // Define New players player Pref in this form ( Just incriment number by1)
			PlayerPrefs.SetFloat ("character 1 jump", 21);
			PlayerPrefs.SetFloat ("character 1 lucky", 1);

			PlayerPrefs.SetInt ("collectableMagnet", 0);           // if(collectableMagnet){ then magnet is enabled and its range=collectableMagnet}

			// only include upgradable fields in this mannner

			//Player Settings
			PlayerPrefs.SetFloat ("0", PlayerPrefs.GetFloat ("character 1 health"));      // Starting health        ,, assigned by curretly active player ie 1
			PlayerPrefs.SetFloat ("1", PlayerPrefs.GetFloat ("character 1 jump"));      // Jump Force                ,, assigned by curretly active player ie 1
			PlayerPrefs.SetFloat ("9", PlayerPrefs.GetFloat ("character 1 lucky"));        // lucky (creates powerups)       ,, assigned by curretly active player ie 1		                                               
	

			// Weapon Settings
			// Missile Settings
			PlayerPrefs.SetFloat ("2", 50);       // Missile Damage 
			PlayerPrefs.SetFloat ("3", 10);       //  Missile Starting Ammo 
			PlayerPrefs.SetFloat ("4", 4);       // Missile Blast Radius

			// Powerup Settings
			// Health Powerup
			PlayerPrefs.SetFloat ("5", 30);        // Health Increased  
			// Ammo Powerup
			PlayerPrefs.SetFloat ("6", 5);         // Ammo Increased
			// Coins Powerup
			PlayerPrefs.SetFloat ("7", 10);         // Coins Increased
			// Slowing Powerup
			PlayerPrefs.SetFloat ("8", 15);          // Slowing Duration

		} 
		else {
			PlayerPrefs.SetInt ("firstTime", 0);
		}
		PlayerPrefs.SetInt("timesLaunched",(PlayerPrefs.GetInt("timesLaunched")+1));     //  incriments by 1 every time the application is launched

		if(!(PlayerPrefs.GetInt("version")==currentVersion)){
			PlayerPrefs.SetInt ("version", currentVersion);

			//  put all the new player pref statements or changes to previously existant in future versions here eg. new players , coin gifts etc

		}
			
		DontDestroyOnLoad (this.gameObject);

		RequestInterstitialAds();

		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
	
		SceneManager.LoadScene("Main Menu");              // transtion from Persistance to main menu scene
	}
		
	public void RestartLevel(){
		SceneManager.LoadScene("Infinite Runner");
	}     

	private void RequestInterstitialAds()
	{

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(instiantialID);

			//***Test***
	/*	AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
			.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
			.Build();  */

		//***Production***
		AdRequest request = new AdRequest.Builder().Build();

		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}


	public void showInterstitialAd()
	{
		//Show Ad
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} 
		else {
		    RequestInterstitialAds ();
		}
	}


	public void LevelToughnessSetter(int level){
		switch (level) {
		case 1:
		case 2:
		case 3:
			playerAcclerationRate=1.1f;
			platformGeneratorType=1;
			enemyHealthMultiplier=1;
			enemyDamageMultiplier=1;
			break;
		case 4:
		case 5:
		case 6:
			playerAcclerationRate=1.2f;
			platformGeneratorType=1;
			enemyHealthMultiplier=1.2f;
			enemyDamageMultiplier=1;
			break;
		case 7:
		case 8:
		case 9:
			playerAcclerationRate=1.3f;
			platformGeneratorType=1;
			enemyHealthMultiplier=1.3f;
			enemyDamageMultiplier=1.2f;
			break;
		case 10:
		case 11:
		case 12:
			playerAcclerationRate=1.4f;
			platformGeneratorType=2;
			enemyHealthMultiplier=1.4f;
			enemyDamageMultiplier=1.4f;
			break;
		case 13:
		case 14:
		case 15:
			playerAcclerationRate=1.5f;
			platformGeneratorType=2;
			enemyHealthMultiplier=1.5f;
			enemyDamageMultiplier=1.4f;
			break;
		case 16:
		case 17:
		case 18:
			playerAcclerationRate=1.6f;
			platformGeneratorType=2;
			enemyHealthMultiplier=1.5f;
			enemyDamageMultiplier=1.4f;
			break;
		case 19:
		case 20:
		case 21:
			playerAcclerationRate=1.7f;
			platformGeneratorType=3;
			enemyHealthMultiplier=1.6f;
			enemyDamageMultiplier=1.5f;
			break;
		case 22:
		case 23:
		case 24:
			playerAcclerationRate=1.8f;
			platformGeneratorType=3;
			enemyHealthMultiplier=1.7f;
			enemyDamageMultiplier=1.6f;
			break;
		case 25:
		case 26:
		case 27:
			playerAcclerationRate=1.9f;
			platformGeneratorType=3;
			enemyHealthMultiplier=2;
			enemyDamageMultiplier=1.7f;
			break;
		case 28:
		case 29:
		case 30:
			playerAcclerationRate=2f;
			platformGeneratorType=3;
			enemyHealthMultiplier=2.2f;
			enemyDamageMultiplier=1.9f;
			break;
		default:
			playerAcclerationRate=2;
			platformGeneratorType=1;
			enemyHealthMultiplier=2;
			enemyDamageMultiplier=2;
			break;
		}
	}
		
}
