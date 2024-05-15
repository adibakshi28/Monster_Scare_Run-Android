using UnityEngine;
using System.Collections;

public class TutorialPlayerScript : MonoBehaviour {

	public float playerSpeed=9f;
	public Vector2 jump;
	public float fireRate=0.5f;
	public GameObject bullet;
	public AudioClip jumpAudio;
	[HideInInspector]
	public int bulletDirection = 1;
	[HideInInspector]
	public bool IsGrounded=true;

	private bool swipeUpStart = false, swipeDownStart = false, fireStart=false,swipeUpStopper=false,swipeDownStopper=false,fireStopper=false;
	Transform firePosition;
	Animator anim;
	bool crouch=false;
	private float jumpRate=0.6f;
	private float nextJump = 0;
	private float nextFire=0;
	GroundPlayerTrigger groundedScript;
	TutorialDataController tutorialDataScript;
	Vector3 lookRotation = new Vector3 (0, 0, -1);
	float xco;
	Rigidbody2D rb;
	AudioSource aud;
	bool isAndroid;
	Vector2 fingerStart,fingerEnd;

	TutorialDamageScript dmTotorial;

	void Start () {
		if (Application.platform == RuntimePlatform.Android) {
			isAndroid = true;                                           
		} 
		else {
			isAndroid = false;
		}

		dmTotorial = GetComponent<TutorialDamageScript> ();
		tutorialDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<TutorialDataController> ();
		groundedScript = GetComponentInChildren<GroundPlayerTrigger> ();
		firePosition = this.gameObject.transform.GetChild(0);
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		aud = GetComponent<AudioSource> ();
	}

	void Update () {
		IsGrounded = groundedScript.IsGrounded;
		anim.SetBool ("Grounded", IsGrounded);

		if (isAndroid) {

			transform.Translate (playerSpeed * Time.deltaTime, 0, 0);  

			if((IsGrounded)&&(!crouch)){
				anim.SetFloat ("Speed", 1);
			}                                                          

			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					fingerStart = touch.position;
					fingerEnd = touch.position;
				}
				if (touch.phase == TouchPhase.Moved) {
					fingerEnd = touch.position;
					if(Mathf.Abs(fingerEnd.y-fingerStart.y)>50){
						if (fingerEnd.y - fingerStart.y > 50) {
							if ((IsGrounded) && (Time.time > nextJump)) {
								if (crouch) {
									anim.SetBool ("Crouch", false);
									crouch = false;
								}
								nextJump = Time.time + jumpRate;
								rb.velocity = jump;
								anim.SetBool ("Grounded", !IsGrounded);
								aud.clip = jumpAudio;
								aud.Play ();
								if ((swipeUpStart)&&(!(swipeUpStopper))) {
									tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "SwipeDown",1);
									swipeUpStopper = true;
								}
							}  
						} else if (fingerEnd.y - fingerStart.y < -50) {
							if ((!crouch)&&(IsGrounded)) {
								StartCoroutine (CrouchSwipe ());
								if ((swipeDownStart)&&(!(swipeDownStopper))) {
									tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "Shoot",2);
									swipeDownStopper = true;
								}
							}
						}
					}
				}
				if (touch.phase == TouchPhase.Ended) {
					fingerEnd = touch.position;
					if(Mathf.Abs(fingerEnd.y-fingerStart.y)>50){
						if(fingerEnd.y-fingerStart.y>50){
							if ((IsGrounded) &&(Time.time>nextJump) ){
								if (crouch) {
									anim.SetBool ("Crouch", false);
									crouch = false;
								}
								nextJump=Time.time+jumpRate;
								rb.velocity = jump;
								anim.SetBool ("Grounded", !IsGrounded);
								aud.clip = jumpAudio;
								aud.Play ();
								if ((swipeUpStart)&&(!(swipeUpStopper))) {
									tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "SwipeDown",1);
									swipeUpStopper = true;
								}
							}  
						}
						else if(fingerEnd.y-fingerStart.y<-50){
							if ((!crouch)&&(IsGrounded)) {
								StartCoroutine (CrouchSwipe ());
								if ((swipeDownStart)&&(!(swipeDownStopper))) {
									tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "Shoot",2);
									swipeDownStopper = true;
								}
							}
						}
					}
				}
			}
		}

		else {
			crouch = false; 
			anim.SetBool ("Crouch", false);

			if ((Input.GetKey (KeyCode.S))&&(IsGrounded)) {
				anim.SetBool ("Crouch", true);
				crouch = true;
				if ((swipeDownStart)&&(!(swipeDownStopper))) {
					tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "Shoot",2);
					swipeDownStopper = true;
				}
			}
	        
			transform.Translate (playerSpeed * Time.deltaTime, 0, 0);  
			anim.SetFloat ("Speed", 1);

			if (Input.GetKeyDown (KeyCode.Space) && (IsGrounded) && (!crouch) && (Time.time > nextJump)) {
				nextJump = Time.time + jumpRate;
				rb.velocity = jump;
				anim.SetBool ("Grounded", true);
				aud.clip = jumpAudio;
				aud.Play ();
				if ((swipeUpStart)&&(!(swipeUpStopper))) {
					tutorialDataScript.TaskCompletor ("GOOD !", 1, 0.5f, "SwipeDown",1);
					swipeUpStopper = true;
				}
			}   

			if (Input.GetKeyDown (KeyCode.F)) {
				if (Time.time > nextFire) {
					nextFire = Time.time + fireRate;
					Fire ();
				}
			}		
		} 
	}

	void Walk(float xco){
		if (xco < 0) {

			Quaternion rotation = Quaternion.LookRotation(lookRotation);
			transform.rotation = rotation;	
			xco = -xco;
		}
		else
		{
			transform.rotation=Quaternion.identity;
		}
		transform.Translate (xco * playerSpeed * Time.deltaTime, 0, 0);   
	}

	void Fire(){
		if ((tutorialDataScript.missilesInTutorial > 0) && (fireStart)) {
			anim.SetTrigger ("Fire");
			tutorialDataScript.missilesInTutorial--;
			tutorialDataScript.missileCountText.text = tutorialDataScript.missilesInTutorial.ToString ();
			if (transform.rotation.y == 0) {
				bulletDirection = 1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			} else {
				bulletDirection = -1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			}
			if (!(fireStopper)) {
				tutorialDataScript.TextWriter3Starter();
				fireStopper = true;
			}
		}
	}


	void OnCollisionEnter2D (Collision2D other){       
		if (other.gameObject.tag == "death") {
			dmTotorial.TakeDamage (1000);	
			}
	}

	public void AndFire(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}
	}

	IEnumerator CrouchSwipe(){
		anim.SetBool ("Crouch", true);
		crouch = true;
		yield return new WaitForSeconds(1);
		anim.SetBool ("Crouch", false);
		crouch = false;
	}    

	public void SpeedSlowerPowerup(int duration,float slowingFactor){
		StartCoroutine (SpeedSlowChanger (duration, slowingFactor));
	}
	IEnumerator SpeedSlowChanger(int duration,float slowingFactor){
		float tempPlayerSpeed = playerSpeed;
		playerSpeed = playerSpeed * slowingFactor;
		yield return new WaitForSeconds(duration);
		playerSpeed = tempPlayerSpeed;
	}

	public void SwipeUpStarter(){
		swipeUpStart = true;
	}
	public void SwipeDownStarter(){
		swipeDownStart = true;
	}
	public void FireStarter(){
		fireStart = true;
	}
}	      
	