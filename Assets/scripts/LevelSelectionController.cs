using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour {

	public AudioClip mainMenuClickAudio;
	public AudioClip themeButtonClickAudio;
	public AudioClip chainRattlingAudio;
	public GameObject canvas,LevelDataFeeder;

	private bool exitMenuActive = false;

	LevelDisplayerScript levelDisplayer;
	GameDataController gameDataScript;

	AudioSource aud;
	Animator anim;

	void Start(){
		aud = GetComponent<AudioSource> ();
		anim = canvas.GetComponent<Animator> ();
		gameDataScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();
		levelDisplayer = LevelDataFeeder.GetComponent<LevelDisplayerScript> ();
		gameDataScript.showInterstitialAd ();            //  show interinstial ads
	}
	void Update(){
		if ((Input.GetKeyDown (KeyCode.Escape))&&(!exitMenuActive)) {
			exitMenuActive = true;
			aud.clip = chainRattlingAudio;
			aud.PlayDelayed (0.15f);
			anim.SetTrigger ("Enter");
		}
	}

	public void L1(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 1;
		gameDataScript.target = 1000;
		gameDataScript.currentLevel = 1;
		gameDataScript.LevelToughnessSetter (1);
		gameDataScript.aimText = levelDisplayer.level [0].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L2(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 2;
		gameDataScript.target = 80;
		gameDataScript.currentLevel = 2;
		gameDataScript.LevelToughnessSetter (2);
		gameDataScript.aimText = levelDisplayer.level [1].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L3(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 3;
		gameDataScript.target = 10;
		gameDataScript.currentLevel = 3;
		gameDataScript.LevelToughnessSetter (3);
		gameDataScript.aimText = levelDisplayer.level [2].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L4(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 4;
		gameDataScript.target = 3;
		gameDataScript.currentLevel = 4;
		gameDataScript.LevelToughnessSetter (4);
		gameDataScript.aimText = levelDisplayer.level [3].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L5(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 5;
		gameDataScript.target = 20;
		gameDataScript.currentLevel = 5;
		gameDataScript.LevelToughnessSetter (5);
		gameDataScript.aimText = levelDisplayer.level [4].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L6(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 6;
		gameDataScript.target = 25;
		gameDataScript.currentLevel = 6;
		gameDataScript.LevelToughnessSetter (6);
		gameDataScript.aimText = levelDisplayer.level [5].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L7(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 7;
		gameDataScript.target = 10;
		gameDataScript.currentLevel = 7;
		gameDataScript.LevelToughnessSetter (7);
		gameDataScript.aimText = levelDisplayer.level [6].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L8(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 8;
		gameDataScript.target = 8;
		gameDataScript.currentLevel = 8;
		gameDataScript.LevelToughnessSetter (8);
		gameDataScript.aimText = levelDisplayer.level [7].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L9(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 11;
		gameDataScript.target = 700;
		gameDataScript.currentLevel = 9;
		gameDataScript.LevelToughnessSetter (9);
		gameDataScript.aimText = levelDisplayer.level [8].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L10(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 10;
		gameDataScript.target = 7;
		gameDataScript.currentLevel = 10;
		gameDataScript.LevelToughnessSetter (10);
		gameDataScript.aimText = levelDisplayer.level [9].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L11(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 1;
		gameDataScript.target = 1700;
		gameDataScript.currentLevel = 11;
		gameDataScript.LevelToughnessSetter (11);
		gameDataScript.aimText = levelDisplayer.level [10].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L12(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 2;
		gameDataScript.target = 150;
		gameDataScript.currentLevel = 12;
		gameDataScript.LevelToughnessSetter (12);
		gameDataScript.aimText = levelDisplayer.level [11].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L13(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 3;
		gameDataScript.target = 15;
		gameDataScript.currentLevel = 13;
		gameDataScript.LevelToughnessSetter (13);
		gameDataScript.aimText = levelDisplayer.level [12].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L14(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 4;
		gameDataScript.target = 5;
		gameDataScript.currentLevel = 14;
		gameDataScript.LevelToughnessSetter (14);
		gameDataScript.aimText = levelDisplayer.level [13].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L15(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 5;
		gameDataScript.target = 30;
		gameDataScript.currentLevel = 15;
		gameDataScript.LevelToughnessSetter (15);
		gameDataScript.aimText = levelDisplayer.level [14].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L16(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 6;
		gameDataScript.target = 35;
		gameDataScript.currentLevel = 16;
		gameDataScript.LevelToughnessSetter (16);
		gameDataScript.aimText = levelDisplayer.level [15].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L17(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 7;
		gameDataScript.target = 15;
		gameDataScript.currentLevel = 17;
		gameDataScript.LevelToughnessSetter (17);
		gameDataScript.aimText = levelDisplayer.level [16].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L18(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 8;
		gameDataScript.target = 12;
		gameDataScript.currentLevel = 18;
		gameDataScript.LevelToughnessSetter (18);
		gameDataScript.aimText = levelDisplayer.level [17].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L19(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 11;
		gameDataScript.target = 1400;
		gameDataScript.currentLevel = 19;
		gameDataScript.LevelToughnessSetter (19);
		gameDataScript.aimText = levelDisplayer.level [18].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L20(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 10;
		gameDataScript.target = 10;
		gameDataScript.currentLevel = 20;
		gameDataScript.LevelToughnessSetter (20);
		gameDataScript.aimText = levelDisplayer.level [19].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L21(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 1;
		gameDataScript.target = 2500;
		gameDataScript.currentLevel = 21;
		gameDataScript.LevelToughnessSetter (21);
		gameDataScript.aimText = levelDisplayer.level [20].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L22(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 2;
		gameDataScript.target = 250;
		gameDataScript.currentLevel = 22;
		gameDataScript.LevelToughnessSetter (22);
		gameDataScript.aimText = levelDisplayer.level [21].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L23(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 3;
		gameDataScript.target = 20;
		gameDataScript.currentLevel = 23;
		gameDataScript.LevelToughnessSetter (23);
		gameDataScript.aimText = levelDisplayer.level [22].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L24(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 4;
		gameDataScript.target = 7;
		gameDataScript.currentLevel = 24;
		gameDataScript.LevelToughnessSetter (24);
		gameDataScript.aimText = levelDisplayer.level [23].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L25(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 5;
		gameDataScript.target = 45;
		gameDataScript.currentLevel = 25;
		gameDataScript.LevelToughnessSetter (25);
		gameDataScript.aimText = levelDisplayer.level [24].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L26(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 6;
		gameDataScript.target = 50;
		gameDataScript.currentLevel = 26;
		gameDataScript.LevelToughnessSetter (26);
		gameDataScript.aimText = levelDisplayer.level [25].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L27(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 7;
		gameDataScript.target = 20;
		gameDataScript.currentLevel = 27;
		gameDataScript.LevelToughnessSetter (27);
		gameDataScript.aimText = levelDisplayer.level [26].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L28(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 8;
		gameDataScript.target = 15;
		gameDataScript.currentLevel = 28;
		gameDataScript.LevelToughnessSetter (28);
		gameDataScript.aimText = levelDisplayer.level [27].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L29(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 11;
		gameDataScript.target = 2000;
		gameDataScript.currentLevel = 29;
		gameDataScript.LevelToughnessSetter (29);
		gameDataScript.aimText = levelDisplayer.level [28].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}
	public void L30(){
		aud.clip = themeButtonClickAudio;
		aud.Play ();
		gameDataScript.gameType = 10;
		gameDataScript.target = 15;
		gameDataScript.currentLevel = 30;
		gameDataScript.LevelToughnessSetter (30);
		gameDataScript.aimText = levelDisplayer.level [29].aimTxt;
		SimpleSceneFader.ChangeSceneWithFade("Infinite Runner");
	}


	public void UpgradeButton(){
		SimpleSceneFader.ChangeSceneWithFade("Upgrade Stuff");
	}
		
	public void ShopButton(){
		SimpleSceneFader.ChangeSceneWithFade("Shop");
	}

	public void TutorialButton(){
		SimpleSceneFader.ChangeSceneWithFade("Story Scene");
	}

	public void ExitButton(){
		Application.Quit ();
		return;
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
