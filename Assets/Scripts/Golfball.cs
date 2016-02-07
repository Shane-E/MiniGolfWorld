using UnityEngine;
using System.Collections;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
	public double ballSpeed = 0;
	public int power = 0;

	// Use this for initialization
	void Start () {
		power = 15;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		//Power of hit increases
		if (ball != null) {
			ball.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 5000);
		}
	}

	void OnMouseUp() {
		//Hit ball using power level.
	}
}
