using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float time = 5;

	void Start () {
		Destroy (this.gameObject, time);
	}
}
