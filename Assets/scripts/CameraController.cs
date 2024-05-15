using UnityEngine;
using System.Collections;
                                                         // this script should be kept disabled . it is enabled by LDC script
public class CameraController : MonoBehaviour {

	public Vector2 margin, smoothing;
	public BoxCollider Bounds;
//	public bool isFollowing { get; set; }
	public float initialXOffset=15;

    Transform player;
	private float CameraOrthographicSize , xAccl,xOffset;
	private Vector3 _min, _max;
	Camera cam;


	void Start () {
		cam = GetComponent<Camera> ();
		CameraOrthographicSize=cam.orthographicSize;
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		_min.x = Mathf.NegativeInfinity;
		_max.x = Mathf.Infinity;
		xOffset = initialXOffset;
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
//		isFollowing = true;	
	}

	void Update () {

		xAccl = Input.acceleration.x;
		xOffset = ((-xAccl) * 7) + initialXOffset;  


		float x = transform.position.x;
		float y = transform.position.y;

			//	if (isFollowing) {
			if (Mathf.Abs (x - player.position.x + xOffset) > margin.x) {
				x = Mathf.Lerp (x, player.position.x + xOffset, smoothing.x * Time.deltaTime);
			}
			if (Mathf.Abs (x - player.position.x) > margin.x) {
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
			}	
	//		}
	  
		var cameraHalfWirth = CameraOrthographicSize * ((float)Screen.width / Screen.height);
		x = Mathf.Clamp (x, _min.x + cameraHalfWirth, _max.x - cameraHalfWirth);
		y = Mathf.Clamp (y, _min.y + CameraOrthographicSize, _max.y - CameraOrthographicSize);
		transform.position = new Vector3 (x,y,transform.position.z);
	
	}

}
