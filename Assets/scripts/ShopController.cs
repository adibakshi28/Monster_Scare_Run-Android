using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CoinPanel
{
	public Text nameText,costText,amountText;
	public int cost,amount;
	public string name;
}

public class ShopController : MonoBehaviour {

	public AudioClip chainRattlingAudio;
	public AudioClip coinPourAudio;
	public GameObject canvas;
	public Text coinText;

	private bool exitMenuActive=false;

	Animator anim;
	AudioSource aud;
	GameDataController gameDataScript;

	public CoinPanel[] coinBuy;

	void Start () {
		gameDataScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();
		anim = canvas.GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();


		coinText.text = PlayerPrefs.GetInt ("coins").ToString ();

		for (int i = 0; i < coinBuy.GetLength (0); i++) {
			coinBuy [i].nameText.text = coinBuy [i].name;
			coinBuy [i].costText.text = "COST: "+coinBuy [i].cost.ToString ();
			coinBuy [i].amountText.text = "+ "+coinBuy [i].amount.ToString ()+" COINS";
		}
			
		gameDataScript.showInterstitialAd ();            //  show interinstial ads
	}

	void Update () {
		if ((Input.GetKeyDown (KeyCode.Escape))&&(!exitMenuActive)) {
			exitMenuActive = true;
			aud.clip = chainRattlingAudio;
			aud.PlayDelayed (0.15f);
			anim.SetTrigger ("Enter");
		}
	}

	public void CoinBuy(int type){                                                                // start this field from 1
		PlayerPrefs.SetInt ("coins",(coinBuy [type - 1].amount)+PlayerPrefs.GetInt ("coins"));
		coinText.text = PlayerPrefs.GetInt ("coins").ToString ();
		aud.clip = coinPourAudio;
		aud.Play ();
	}


	public void BackButton(){
		SimpleSceneFader.ChangeSceneWithFade("Level Select");
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

