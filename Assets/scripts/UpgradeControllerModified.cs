using UnityEngine;
using System.Collections;
using UnityEngine.UI;
                                           
// to create new fields of upgrade make sure that field vale in player pref is same as passed vale in button arguement is also same as inded in array (of upgrades in this script)

[System.Serializable]
public class Upgrade
{
	public GameObject fieldPanel;
	public int cost, maxFieldValue;
	public float valueIncreased;
	[HideInInspector]
	public UpgradeFieldPanel fieldPanelScript;
	[HideInInspector]
	public Slider currentValueSlider,upgradedValueSlider;
	[HideInInspector]
	public Button upgradeButton;
	[HideInInspector]
	public Text currentValueText,upgradedValueText,costText;
	[HideInInspector]
	public float currentValue,upgradedValue;
}

public class UpgradeControllerModified : MonoBehaviour {

	public AudioClip chainRattlingAudio;
	public AudioClip coinPourAudio;
	public GameObject canvas;

	public GameObject playerPanel, playerDemo;
	public GameObject weaponsPanel, weaponDemo;
	public GameObject powerupPanel, powerupDemo;

	public Text coinText;

	public Upgrade[] upgrade;

	private bool exitMenuActive = false;

	Animator anim;
	AudioSource aud;
	GameDataController gameDataScript;

	void Awake(){
		anim = canvas.GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();
	}

	void Start () {

		gameDataScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();

		coinText.text = PlayerPrefs.GetInt ("coins").ToString ();

		for (int field = 0; field < upgrade.GetLength (0); field++) {
			
			upgrade [field].fieldPanelScript = upgrade [field].fieldPanel.GetComponent<UpgradeFieldPanel> ();

			upgrade[field].currentValueSlider=upgrade [field].fieldPanelScript.currentValueSlider;
			upgrade[field].upgradedValueSlider=upgrade [field].fieldPanelScript.upgradedValueSlider;
			upgrade [field].upgradeButton = upgrade [field].fieldPanelScript.upgradeButton;
			upgrade[field].costText=upgrade [field].fieldPanelScript.costText;
			upgrade[field].currentValueText=upgrade [field].fieldPanelScript.currentValueText;
			upgrade[field].upgradedValueText=upgrade [field].fieldPanelScript.upgradedValueText;

			upgrade [field].currentValueSlider.maxValue = upgrade [field].maxFieldValue;
			upgrade [field].upgradedValueSlider.maxValue = upgrade [field].maxFieldValue;

			upgrade [field].currentValue = PlayerPrefs.GetFloat (field.ToString ());
			upgrade [field].upgradedValue = upgrade [field].currentValue + upgrade [field].valueIncreased;
			upgrade [field].currentValueSlider.value = upgrade [field].currentValue;
			if (upgrade [field].currentValueSlider.maxValue <= upgrade [field].currentValue) {
				upgrade [field].upgradeButton.interactable = false;
				upgrade [field].upgradedValueSlider.value = upgrade [field].currentValue;
				upgrade [field].upgradedValue = 0;
				upgrade [field].cost = 0;
				upgrade [field].costText.text = "-";
				upgrade [field].currentValueText.text = upgrade [field].currentValue.ToString ()+" (MAX VALUE)";
				upgrade [field].upgradedValueText.text = upgrade [field].upgradedValue.ToString ()+" (MAX VALUE)";
			} 
			else {
				upgrade [field].upgradeButton.interactable = true;
				upgrade [field].upgradedValueSlider.value = upgrade [field].upgradedValue;
				upgrade [field].costText.text = upgrade [field].cost.ToString ();
				upgrade [field].currentValueText.text = upgrade [field].currentValue.ToString ();
				upgrade [field].upgradedValueText.text = upgrade [field].upgradedValue.ToString ();
			}
		}
			
		gameDataScript.showInterstitialAd ();             //  show interinstial ads

	}

	void Update () {
		if ((Input.GetKeyDown (KeyCode.Escape))&&(!exitMenuActive)) {
			exitMenuActive = true;
			aud.clip = chainRattlingAudio;
			aud.PlayDelayed (0.15f);
			anim.SetTrigger ("Enter");
		}
	}

	public void PlayerUpgrade(){
		playerPanel.SetActive (true);
		playerDemo.SetActive (true);
		weaponsPanel.SetActive (false);
		weaponDemo.SetActive (false);
		powerupPanel.SetActive (false);
		powerupDemo.SetActive (false);
	}

	public void WeaponUpgrade(){
		playerPanel.SetActive (false);
		playerDemo.SetActive (false);
		weaponsPanel.SetActive (true);
		weaponDemo.SetActive (true);
		powerupPanel.SetActive (false);
		powerupDemo.SetActive (false);
	}

	public void PowerupUpgrade(){
		playerPanel.SetActive (false);
		playerDemo.SetActive (false);
		weaponsPanel.SetActive (false);
		weaponDemo.SetActive (false);
		powerupPanel.SetActive (true);
		powerupDemo.SetActive (true);
	}


	public void UpgradeField(int field){                                        // assign this function yo upgrade button in field and pass its indice as auguement
		
		if ((PlayerPrefs.GetInt ("coins") - upgrade[field].cost) >= 0) {
			PlayerPrefs.SetInt ("coins", (PlayerPrefs.GetInt ("coins") - upgrade[field].cost));
			coinText.text = PlayerPrefs.GetInt ("coins").ToString ();
			upgrade [field].currentValueSlider.value = upgrade [field].upgradedValue;
			upgrade [field].currentValue = upgrade [field].upgradedValue;
			upgrade [field].upgradedValue += upgrade [field].valueIncreased;
			if (upgrade [field].currentValueSlider.maxValue <= upgrade [field].currentValue) {
				upgrade [field].upgradeButton.interactable = false;
				upgrade [field].costText.text = "-";
				upgrade [field].currentValueText.text = upgrade [field].currentValue.ToString ()+" (MAX VALUE)";
				upgrade [field].upgradedValueText.text = upgrade [field].upgradedValue.ToString ()+" (MAX VALUE)";
			} 
			else {
				upgrade [field].upgradedValueSlider.value = upgrade [field].upgradedValue;
				upgrade [field].currentValueText.text = upgrade [field].currentValue.ToString ();
				upgrade [field].upgradedValueText.text = upgrade [field].upgradedValue.ToString ();
			}
			PlayerPrefs.SetFloat (field.ToString (), upgrade [field].currentValue);

			aud.clip = coinPourAudio;
			aud.Play ();
			                                                                              // Include all Player Related Fields in Following manner in if statements
			if (field == 0) {                                                                 
				PlayerPrefs.SetFloat ("character " + PlayerPrefs.GetInt ("activePlayer") + " health", PlayerPrefs.GetFloat (field.ToString ()));
			}
			if (field == 1) {
				PlayerPrefs.SetFloat ("character " + PlayerPrefs.GetInt ("activePlayer") + " jump", PlayerPrefs.GetFloat (field.ToString ()));
			}
			if (field == 9) {
				PlayerPrefs.SetFloat ("character " + PlayerPrefs.GetInt ("activePlayer") + " lucky", PlayerPrefs.GetFloat (field.ToString ()));
			}
		}
	}

	public void BackButton(){
		SimpleSceneFader.ChangeSceneWithFade("Level Select");
	}

	public void ShopButton(){
		SimpleSceneFader.ChangeSceneWithFade("Shop");
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
