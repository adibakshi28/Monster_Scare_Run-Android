using UnityEngine;
using System.Collections;
using DigitalRuby.LightningBolt;

public class LighteningController : MonoBehaviour {

	public float lighteningRate=5;
	public float lighteningStartTime=5;

	LineRenderer lr;
	LightningBoltScript lighteningScript;
	AudioSource aud;

	void Start () {
		lr = GetComponent<LineRenderer> ();
		lighteningScript = GetComponent<LightningBoltScript> ();
		aud = GetComponent<AudioSource> ();
		lr.enabled = false;
		lighteningScript.enabled = false;
		InvokeRepeating ("LighteningGenerator", lighteningStartTime, lighteningRate);
	}

	void LighteningGenerator(){
		lighteningScript.StartPosition.x = Random.Range (-14f, -2f);
		lighteningScript.StartPosition.y = Random.Range (1f, 10f);
		lighteningScript.EndPosition.x = Random.Range (2f, 14f);
		lighteningScript.EndPosition.y = Random.Range (0f, 10f);
        
		StartCoroutine (LighteningInstantor ());
	}

	IEnumerator LighteningInstantor(){
		lr.enabled = true;
		lighteningScript.enabled = true;
		aud.Play ();
	    yield return new WaitForSeconds(0.7f);
		lr.enabled = false;
		lighteningScript.enabled = false;
	}

}
