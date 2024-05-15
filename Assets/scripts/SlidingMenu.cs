using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SlidingMenu : MonoBehaviour {
	
	public Canvas YourCanvas;
	public Image backgroundImage;
	public Material imageGreen;
	public Material imageBlue;
	public Material imageBlack;
	public Material imageViolet;


	//ScrollBar Settings-----------------------------------------------------------------------------------------
	public Scrollbar HorizontalScrollBar;
	private float k=0;
	private bool ButtonClicked=false;
	//-----------------------------------------------------------------------------------------------------------

	//Content Settings-------------------------------------------------------------------------------------------
	public RectTransform ScrollContent;
//	public List<GameObject> LevelThumbnails;
	public List<Button> LevelThumbnails;
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

	AudioSource aud;

	void Start () {
		aud = GetComponent<AudioSource> ();
		//Auto Find Slides And Auto Set Size And Position Of Slides
		for (int b=0; b<LevelThumbnails.Count; b++) {
			LevelThumbnails[b].GetComponent<RectTransform>().sizeDelta=new Vector2(Element_Width,Element_Height);
			LevelThumbnails[b].GetComponent<RectTransform>().localPosition=new Vector3((2*b+3)*Element_Width/2+(2*b+3)*Element_Margin,0,10);
		}
		//-------------------------------------------------------------------------------------------

		//Set Size Of ScrollContent (Auto Set)
		ScrollContent.GetComponent<RectTransform>().sizeDelta=new Vector2((LevelThumbnails.Count+2)*(Element_Width+2*Element_Margin),Element_Height);
		//-------------------------------------------------------------------------------------------

		HorizontalScrollBar.gameObject.SetActive(false);             

		//Calculate ScrollSteps Value----------------------------------------------------------------
		n = LevelThumbnails.Count - 1;
		ScrollSteps = 1 / n;
		//-------------------------------------------------------------------------------------------
	}



	void Update () {

		//Slides Magnet------------------------------------------------------------------------------
		if (DesktopPlatform == false) {
			if (Input.touchCount == 0) {
				for (int s=0; s<LevelThumbnails.Count; s++) {
					if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
						HorizontalScrollBar.GetComponent<Scrollbar> ().value = Mathf.Lerp (HorizontalScrollBar.GetComponent<Scrollbar> ().value, s * ScrollSteps, 0.1f);
					}
				}
			}
		} 
		//When Use Next And Previous Buttons
		for (int s=0; s<LevelThumbnails.Count; s++) {
			if (k > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && k <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
				k = Mathf.Lerp (k, s * ScrollSteps, 0.1f);
			}
		}
		//-------------------------------------------------------------------------------------------



		//Slides Scale, Slides Transition And Slides Color Transition-------------------------------
		for (int s=0; s<LevelThumbnails.Count; s++) {
			for (int t=0; t<LevelThumbnails.Count; t++) {
				if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
					if(t!=s){
						LevelThumbnails [t].transform.localScale = Vector2.Lerp (LevelThumbnails [t].transform.localScale, new Vector2 (1, 1), Transition_Out);
						LevelThumbnails [t].interactable = false;
					}
					if(t==s){
						LevelThumbnails [t].interactable = true;
						LevelThumbnails [s].transform.localScale = Vector2.Lerp (LevelThumbnails [s].transform.localScale, new Vector2 (Element_Scale, Element_Scale), Transition_In);
						LevelThumbnails [s].gameObject.transform.SetAsLastSibling();
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
						}
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
		aud.Play ();
		k = Mathf.Clamp (k+ScrollSteps,0,1);
		ButtonClicked = true;
	}



	public void PreviousButton(){
		aud.Play ();
		k = Mathf.Clamp (k-ScrollSteps,0,1);
		ButtonClicked = true;
	}



	public void ContentDrag(){
		ButtonClicked = false;
		k=Mathf.Clamp (HorizontalScrollBar.GetComponent<Scrollbar> ().value,0,1);

	}
}
		