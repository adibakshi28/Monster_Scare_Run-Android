using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
	
	public float playerSpeed=9f;
	public float accletationRate=1.1f;
	public Vector2 jump;
	public float fireRate=0.5f;
	public GameObject bullet;
	public GameObject flamethrower;
	public GameObject laser;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public int bulletDirection = 1;
	[HideInInspector]
	public bool flamethrow = false;
	[HideInInspector]
	public bool laserShoot = false;
	[HideInInspector]
	public bool IsGrounded=true;

	bool crouch=false;
    Transform firePosition;
	private GameObject flameThrower;
	private GameObject laserGun;
	private float jumpRate=0.6f;
	private float nextJump = 0;
	private float nextFire=0;
	GroundPlayerTrigger groundedScript;
	LevelDataController levelDataScript;
	Vector3 lookRotation = new Vector3 (0, 0, -1);
	float xco;
	Rigidbody2D rb;
	GameObject coinMagnet;
	CircleCollider2D coinMagnetTrigger;
	bool isAndroid;
//	bool move=false;
	int direction=0;
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
		firePosition = this.gameObject.transform.GetChild(2);
		coinMagnet = this.gameObject.transform.GetChild (4).gameObject;
		coinMagnetTrigger = coinMagnet.GetComponent<CircleCollider2D> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		jump.y=PlayerPrefs.GetFloat ("jumpForce");
		accletationRate = levelDataScript.playerAcclerationRate;

		if (!(PlayerPrefs.GetInt("collectableMagnet")==0)) {
			
			coinMagnet.SetActive (true);
			coinMagnetTrigger.radius = PlayerPrefs.GetInt ("collectableMagnet");
		} else {
			coinMagnet.SetActive (false);
		}

	}

    void Update () {

		playerSpeed = playerSpeed + (playerSpeed * (accletationRate / 1000)*Time.deltaTime);

		IsGrounded = groundedScript.IsGrounded;
		anim.SetBool ("Grounded", IsGrounded);

		if (isAndroid) {
			
	/*		if (move) {
				AndWalk ();
			}               */

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
		}

		else {
			crouch = false; 
			anim.SetBool ("Crouch", false);

			if ((Input.GetKey (KeyCode.S))&&(IsGrounded)) {
				anim.SetBool ("Crouch", true);
				crouch = true;
			}

		/*	if (!crouch) {
				xco = Input.GetAxis ("Horizontal");
				anim.SetFloat ("Speed", Mathf.Abs (xco));
				if ((xco != 0)) {
					Walk (xco);
				}
			}    */   transform.Translate (playerSpeed * Time.deltaTime, 0, 0);  

			if (Input.GetKeyDown (KeyCode.Space) && (IsGrounded) && (!crouch) && (Time.time > nextJump)) {
				nextJump = Time.time + jumpRate;
				rb.velocity = jump;
				anim.SetBool ("Grounded", true);
			}   

			if (Input.GetKeyDown (KeyCode.F)) {
				if (Time.time > nextFire) {
					nextFire = Time.time + fireRate;
					Fire ();
				}
			}		

			if ((Input.GetKeyDown (KeyCode.H))&&((!flamethrow))){
					FlameFire ();
			}
			if ((Input.GetKeyUp (KeyCode.H))&&(flamethrow)) {
				Destroy (flameThrower);
				flamethrow = false;
				anim.SetBool ("Flamethrow", flamethrow);        
			}


			if ((Input.GetKeyDown (KeyCode.L))&&((!laserShoot))){
				LaserFire ();
			}
			if ((Input.GetKeyUp (KeyCode.L))&&(laserShoot)) {
				Destroy (laserGun);
				laserShoot = false;
				anim.SetBool ("Flamethrow", flamethrow);
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

	 void AndWalk(){
		if (direction < 0) {
			Quaternion rotation = Quaternion.LookRotation(lookRotation);
			transform.rotation = rotation;	
		}
		else
		{
			transform.rotation=Quaternion.identity;
		}
		transform.Translate (playerSpeed * Time.deltaTime, 0, 0); 
	}

	void Fire(){
		if (levelDataScript.missilesInLevel > 0) {
			anim.SetTrigger ("Shoot");
			levelDataScript.missilesInLevel--;
			levelDataScript.missileCountText.text = levelDataScript.missilesInLevel.ToString ();
			if (transform.rotation.y == 0) {
				bulletDirection = 1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			} else {
				bulletDirection = -1;
				Instantiate (bullet, firePosition.position, Quaternion.identity);
			}
		}
	}

	void FlameFire(){
		anim.SetTrigger ("Shoot");
		flamethrow = true;
		anim.SetBool ("Flamethrow", flamethrow);
		if (transform.rotation.y == 0) {
			 flameThrower = Instantiate (flamethrower, new Vector3 (transform.position.x, transform.position.y, -1), new Quaternion (0, 180, 0, 0))as GameObject;
		} else {
			 flameThrower = Instantiate (flamethrower, new Vector3 (transform.position.x, transform.position.y, -1), new Quaternion (0, 0, 0, 0))as GameObject;
		}
		flameThrower.transform.parent = this.gameObject.transform.GetChild (2);
	}

	void LaserFire(){
		anim.SetTrigger ("Shoot");
		laserShoot = true;                               
		anim.SetBool ("Flamethrow", flamethrow);           // using same variable as functionality is same
		if (transform.rotation.y == 0) {
			laserGun = Instantiate (laser, new Vector3 (transform.position.x, transform.position.y, -1), new Quaternion (0, 0, 0, 0))as GameObject;
		} else {
			laserGun = Instantiate (laser, new Vector3 (transform.position.x, transform.position.y, -1), new Quaternion (0, 180, 0, 0))as GameObject;
		}
		laserGun.transform.parent = this.gameObject.transform.GetChild (2);
	}

	void OnCollisionEnter2D (Collision2D other){       
		if (other.gameObject.tag == "death") {
			dm.TakeDamage (10000);	
		}
	}

	public void AndFire(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}
	}
/*	public void AndCrouch(){
		Debug.Log ("crouch");
		if (IsGrounded) {
			anim.SetBool ("Crouch", true);
			crouch = true;
		}
	}
       
    public void AndCrouchRelease(){
		Debug.Log ("crouch release");
	    anim.SetBool ("Crouch", false);
	    crouch = false;
    }    

	public void AndJump(){
		if ((IsGrounded) && (!crouch)&&(Time.time>nextJump) ){
			nextJump=Time.time+jumpRate;
			rb.velocity = jump;
			anim.SetBool ("Grounded", !IsGrounded);
		}  
	}
	public void AndMove(int dir){
		Debug.Log ("1");
		direction = dir;
		move = true;
		anim.SetFloat ("Speed", Mathf.Abs (dir));
	}
	public void AndMoveRelease(){
		Debug.Log ("0");
		move = false;
		anim.SetFloat ("Speed", 0);
	}             */

	IEnumerator CrouchSwipe(){
		anim.SetBool ("Crouch", true);
		crouch = true;
		yield return new WaitForSeconds(0.75f);
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

}	      
