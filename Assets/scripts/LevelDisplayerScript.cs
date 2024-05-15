using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Level
{
	public GameObject levelThumbnail; 
	public string aimTxt, lvlName;
	[HideInInspector]
	public GameObject unlockLevelButton;
	[HideInInspector]
	public BtnDataFeeder btnDataScript;
	[HideInInspector]
	public Button levelButton;
	[HideInInspector]
	public Text highScoreText,aimText,lvlText;
}

public class LevelDisplayerScript : MonoBehaviour {
	
	public AudioClip coinPourAudio, transtionAudio;

	public Canvas YourCanvas;
	//	public Image backgroundImage;

	public Text coinText;

	//ScrollBar Settings-----------------------------------------------------------------------------------------
	public Scrollbar HorizontalScrollBar;
	private float k=0;
	private bool ButtonClicked=false;
	public GameObject previousButton, nextButton;
	//-----------------------------------------------------------------------------------------------------------

	//Content Settings-------------------------------------------------------------------------------------------
	public RectTransform ScrollContent;

	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public float Element_Width;
	public float Element_Height;
	public float Element_Margin;
	public float Element_Scale;
	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public float Transition_In;
	public float Transition_Out;
	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public bool DesktopPlatform;
	//-----------------------------------------------------------------------------------------------------------

	//Other Variables--------------------------------------------------------------------------------------------
	private float n;
	private float ScrollSteps;
	//-----------------------------------------------------------------------------------------------------------

	public Level[] level;

	AudioSource aud;
	GameDataController gameControllerScript;

	void Awake(){
		for (int lvl = 0; lvl < level.GetLength (0); lvl++) {
			level [lvl].btnDataScript = level [lvl].levelThumbnail.GetComponent<BtnDataFeeder> ();

			level [lvl].levelButton = level [lvl].btnDataScript.levelButton;
			level [lvl].unlockLevelButton = level [lvl].btnDataScript.unlockLevelButton;
			level [lvl].aimText = level [lvl].btnDataScript.aimText;
			level [lvl].aimText.text = level [lvl].aimTxt;
			level [lvl].lvlText = level [lvl].btnDataScript.lvlText;
			level [lvl].lvlText.text = level [lvl].lvlName;
			level [lvl].highScoreText = level [lvl].btnDataScript.highScoreText;
		}
		level [0].unlockLevelButton.SetActive (false); 
	}

	void Start () {
		gameControllerScript = GameObject.FindWithTag ("gameDataController").GetComponent<GameDataController> ();
		aud = GetComponent<AudioSource> ();
		//Auto Find Slides And Auto Set Size And Position Of Slides

		for (int b=0; b<level.GetLength(0); b++) {
			level[b].levelButton.GetComponent<RectTransform>().sizeDelta=new Vector2(Element_Width,Element_Height);
			level[b].levelButton.GetComponent<RectTransform>().localPosition=new Vector3((2*b+3)*Element_Width/2+(2*b+3)*Element_Margin,0,10);
		}
		//-------------------------------------------------------------------------------------------
		coinText.text = PlayerPrefs.GetInt ("coins").ToString ();

		//Set Size Of ScrollContent (Auto Set)
		ScrollContent.GetComponent<RectTransform>().sizeDelta=new Vector2((level.GetLength(0)+2)*(Element_Width+2*Element_Margin),Element_Height);
		//-------------------------------------------------------------------------------------------

		HorizontalScrollBar.gameObject.SetActive(false);             

		//Calculate ScrollSteps Value----------------------------------------------------------------
		n = level.GetLength(0) - 1;
		ScrollSteps = 1 / n;
		//-------------------------------------------------------------------------------------------

		for (int lvl = 1; lvl < level.GetLength(0)+1; lvl++) {
			level[lvl-1].highScoreText.text = "RECORD : " + (PlayerPrefs.GetInt("Level "+lvl.ToString()+" Highscore")).ToString();
		}

		gameControllerScript.totalLevelsInGame = (level.GetLength(0));

		StartCoroutine (LevelToPlay ());
	}

	void Update () {

		//Slides Magnet------------------------------------------------------------------------------
		if (DesktopPlatform == false) {
			if (Input.touchCount == 0) {
				for (int s=0; s<level.GetLength(0); s++) {
					if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
						HorizontalScrollBar.GetComponent<Scrollbar> ().value = Mathf.Lerp (HorizontalScrollBar.GetComponent<Scrollbar> ().value, s * ScrollSteps, 0.1f);
					}
				}
			}
		} 
		//When Use Next And Previous Buttons
		for (int s=0; s<level.GetLength(0); s++) {
			if (k > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && k <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
				k = Mathf.Lerp (k, s * ScrollSteps, 0.1f);
			}
		}
		//-------------------------------------------------------------------------------------------

		//Slides Scale, Slides Transition And Slides Color Transition-------------------------------
		for (int s=0; s<level.GetLength(0); s++) {
			for (int t=0; t<level.GetLength(0); t++) {
				if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
					if(t!=s){
						level[t].levelButton.transform.localScale = Vector2.Lerp (level[t].levelButton.transform.localScale, new Vector2 (1, 1), Transition_Out);
						level[t].levelButton.interactable = false;
						level[t].unlockLevelButton.SetActive (false);
					}
					if(t==s){
						if (PlayerPrefs.GetInt ("levelReached") >= (s + 1)) {      
							level[t].levelButton.interactable = true;

						} else {
							if ((PlayerPrefs.GetInt ("levelReached")+1) == (s + 1)) {
								level[t].levelButton.interactable = false;
								level[t].unlockLevelButton.SetActive (true);
							} 
							else {
								level[t].levelButton.interactable = false;
								level[t].unlockLevelButton.SetActive (false);
							}
						}

						if ((t == 0) || (t == (level.GetLength (0) - 1))) {
							if (t == 0) {
								previousButton.SetActive (false);
							}
							else {
								nextButton.SetActive (false);
							}
						}
						else {
							nextButton.SetActive (true);
							previousButton.SetActive (true);
						}

						level[s].levelButton.transform.localScale = Vector2.Lerp (level[s].levelButton.transform.localScale, new Vector2 (Element_Scale, Element_Scale), Transition_In);
						level[s].levelButton.gameObject.transform.SetAsLastSibling();
						/*				
						switch (s) {
						case 0: 
							backgroundImage.material = imageGreen;
							break;
						case 1: 
							backgroundImage.material = imageBlue;
							break;
						case 2: 
							backgroundImage.material = imageBlack;
							break;
						case 3: 
							backgroundImage.material = imageViolet;
							break;
						default:
							backgroundImage.material = null;
							break;
						}         */
					}
				}
			}                                    
		}
		//-------------------------------------------------------------------------------------------


		//Next Or Previous Button Is Clicked---------------------------------------------------------
		if (ButtonClicked == true) {
			HorizontalScrollBar.GetComponent<Scrollbar> ().value=Mathf.Lerp(HorizontalScrollBar.GetComponent<Scrollbar> ().value,k,0.1f);
		}
		//-------------------------------------------------------------------------------------------
	}


	public void NextButton(){
		aud.clip = transtionAudio;
		aud.Play ();
		k = Mathf.Clamp (k+ScrollSteps,0,1);
		ButtonClicked = true;
	}


	public void PreviousButton(){
		aud.clip = transtionAudio;
		aud.Play ();
		k = Mathf.Clamp (k-ScrollSteps,0,1);
		ButtonClicked = true;
	}


	public void ContentDrag(){
		ButtonClicked = false;
		k=Mathf.Clamp (HorizontalScrollBar.GetComponent<Scrollbar> ().value,0,1);

	}

	public void UnlockForCoins(int levelToUnlock){
		int coinCost = 1000;

		if ((PlayerPrefs.GetInt ("coins") - coinCost) >= 0) {
			PlayerPrefs.SetInt ("coins", (PlayerPrefs.GetInt ("coins") - coinCost));
			PlayerPrefs.SetInt ("levelReached", levelToUnlock); 
			aud.clip = coinPourAudio;
			aud.Play ();
			level[levelToUnlock - 1].unlockLevelButton.SetActive (false);              
			coinText.text = PlayerPrefs.GetInt ("coins").ToString ();
		} 
		else {

		}
	}  

	IEnumerator LevelToPlay(){
		yield return new WaitForSeconds(0.3f);
		for (int lvl = 1; lvl < PlayerPrefs.GetInt ("levelReached"); lvl++) {
			NextButton ();
			yield return new WaitForSeconds(0.05f);
		}
	}
}
