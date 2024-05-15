using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float playerSpeed=10f;
	public float accletationRate = 1.1f, foneTiltAccRate = 3;
	public Vector2 jump;
	public float fireRate=0.5f;
	public GameObject bullet;
	public GameObject ammoIncPowerup;
	public AudioClip jumpAudio;
	[HideInInspector]
	public int bulletDirection = 1;
	[HideInInspector]
	public bool IsGrounded=true;

	GameObject coinMagnet;
	Transform firePosition,ammoIncCreationPosition;
	Animator anim;
	bool crouch=false;
	private float jumpRate=0.6f;
	private float nextJump = 0;
	private float nextFire=0;
	GroundPlayerTrigger groundedScript;
	LevelDataController levelDataScript;
	Vector3 lookRotation = new Vector3 (0, 0, -1);
	float xco,xAccl;
	Rigidbody2D rb;
	AudioSource aud;
	CircleCollider2D coinMagnetTrigger;
	bool isAndroid,dead=false;
	Vector2 fingerStart,fingerEnd;

	DamageScript dm;

	void Start () {
		if (Application.platform == RuntimePlatform.Android) {
			isAndroid = true;                                           
		} 
		else {
			isAndroid = false;
		}

		dm = GetComponent<DamageScript> ();
		levelDataScript = GameObject.FindWithTag ("levelDataController").GetComponent<LevelDataController> ();
		groundedScript = GetComponentInChildren<GroundPlayerTrigger> ();
		firePosition = this.gameObject.transform.GetChild(0);
		ammoIncCreationPosition = this.gameObject.transform.GetChild(2);     // just to acess its x coodinate
		coinMagnet = this.gameObject.transform.GetChild (1).gameObject;
		coinMagnetTrigger = coinMagnet.GetComponent<CircleCollider2D> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		aud = GetComponent<AudioSource> ();

		jump.y=PlayerPrefs.GetFloat ("1");
		accletationRate = levelDataScript.playerAcclerationRate;

		if (!(PlayerPrefs.GetInt("collectableMagnet")==0)) {

			coinMagnet.SetActive (true);
			coinMagnetTrigger.radius = PlayerPrefs.GetInt ("collectableMagnet");
		} else {
			coinMagnet.SetActive (false);
		}      

	}

	void Update () {

		playerSpeed = playerSpeed + (playerSpeed * (accletationRate / 300)*Time.deltaTime);

		IsGrounded = groundedScript.IsGrounded;
		anim.SetBool ("Grounded", IsGrounded);

		if (isAndroid) {

			xAccl = Input.acceleration.x;
			playerSpeed = (xAccl*foneTiltAccRate) + playerSpeed;
			//		Debug.Log (playerSpeed);

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
							}  
						} else if (fingerEnd.y - fingerStart.y < -50) {
							if ((!crouch)&&(IsGrounded)) {
								StartCoroutine (CrouchSwipe ());
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
							}  
						}
						else if(fingerEnd.y-fingerStart.y<-50){
							if ((!crouch)&&(IsGrounded)) {
								StartCoroutine (CrouchSwipe ());
							}
						}
					}
				}
			}

			playerSpeed = playerSpeed - (xAccl * foneTiltAccRate);
		
		}

		else {
			crouch = false; 
			anim.SetBool ("Crouch", false);

			if ((Input.GetKey (KeyCode.S))&&(IsGrounded)) {
				anim.SetBool ("Crouch", true);
				crouch = true;
			}
	/*			if (!crouch) {
				xco = Input.GetAxis ("Horizontal");
				characterAnim.SetFloat ("Speed", Mathf.Abs (xco));
				if ((xco != 0)) {
					Walk (xco);
				}
			}     */  transform.Translate (playerSpeed * Time.deltaTime, 0, 0);  
			anim.SetFloat ("Speed", 1);

			if (Input.GetKeyDown (KeyCode.Space) && (IsGrounded) && (!crouch) && (Time.time > nextJump)) {
				nextJump = Time.time + jumpRate;
				rb.velocity = jump;
				aud.clip = jumpAudio;
				aud.Play ();
				anim.SetBool ("Grounded", true);
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
		if (levelDataScript.missilesInLevel > 0) {
			anim.SetTrigger ("Fire");
			levelDataScript.missilesInLevel--;
			levelDataScript.missileCountText.text = levelDataScript.missilesInLevel.ToString ();
			if (transform.rotation.y == 0) {
				bulletDirection = 1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			} else {
				bulletDirection = -1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			}
			AmmoIncreasor ();
		} 
	}
		

	void OnCollisionEnter2D (Collision2D other){       
		if (other.gameObject.tag == "death") {
			if (!dead) {
				dm.TakeDamage (1000);	
				dead = true;
			}
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

	public void AmmoIncreasor(){
		if ((Random.Range (19, 91) % (7-(int)(PlayerPrefs.GetFloat("9")))==0)){
			Vector3 instPosition = new Vector3 (ammoIncCreationPosition.position.x-10, Random.Range (3,16), 0);
			Instantiate (ammoIncPowerup,instPosition,Quaternion.identity);
		}
	}

}	      

