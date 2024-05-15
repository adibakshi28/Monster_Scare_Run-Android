using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {

	public Text displayText;

	private int sequence=1;

	void Start () {
		SequenceContoller ();
	}

	IEnumerator TextWriter1(string text,float displayTime,float displayOffsetTime,int functionToCall){
		sequence++;
		yield return new WaitForSeconds(displayOffsetTime);
		for (int i = 0; i < text.Length; i++) {
			displayText.text = displayText.text.ToString () + text [i].ToString ();
			yield return new WaitForSeconds(0.08f);
		}
		yield return new WaitForSeconds(displayTime);
		displayText.text = "";
		if (functionToCall==1) {
			SequenceContoller();
		}
		else{
			SkipStory();
		}
	}

	void SequenceContoller(){
		switch (sequence) {
		case 1:
			StartCoroutine (TextWriter1 ("ONCE THIS LAND BELONGED TO A VERY WEALTHY FAMILY .\n              THEY LIVED HAPPILY.....",2,1,1));
			break;
		case 2:
			StartCoroutine (TextWriter1 ("UNTIL",1,0.2f,1));
			break;
		case 3:
			StartCoroutine (TextWriter1 ("THEY WERE BRUTALLY MURDERED AS THEY PRACTICED BLACK MAGIC",2,0.5f,1));
			break;
		case 4:
			StartCoroutine (TextWriter1 ("ALL THEIR MASSIVE LAND WAS CONVERTED INTO A GRAVEYARD",2,1,1));
			break;
		case 5:
			StartCoroutine (TextWriter1 ("WITH THAT FAMILY BEING THE FIRST TO BE BURIED THERE",2,1,1));
			break;
		case 6:
			StartCoroutine (TextWriter1 ("ALL THE THEIR RICHES ARE STILL BELIEVED TO BE SCATTERED HERE",2,1,1));
			break;
		case 7:
			StartCoroutine (TextWriter1 ("BUT NO ONE HAS EVER DARED TO LOOK FOR THEM",2,1,1));
			break;
		case 8:
			StartCoroutine (TextWriter1 ("I BELIEVE YOU ARE BRAVE ENOUGH !",2,1,1));
			break;
		case 9:
			StartCoroutine (TextWriter1 ("BUT BEWARE........",2,0.5f,2));
			break;
		}
	}

	public void SkipStory(){
		SimpleSceneFader.ChangeSceneWithFade("Tutorial");
	}

	public void CheatAddCoins(int coinsToAdd){
		PlayerPrefs.SetInt ("coins",PlayerPrefs.GetInt ("coins")+coinsToAdd);
	}
}
