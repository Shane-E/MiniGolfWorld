using UnityEngine;
using System.Collections;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
	private bool isMoving = false;
    private float ballDrag;
	public int minHitPower = 500;
    public int maxHitPower = 2750;

    // Use this for initialization
    void Start() {
        ballDrag = ball.GetComponent<Rigidbody>().angularDrag = 1;
    }
	
	// Update is called once per frame
	void Update () {
        //Allow the ball to be hit if the ball is not null, the ball is not currently moving, and the left mouse button is clicked.
        if (ball != null && Input.GetMouseButtonDown(0) && !isMoving)
        {
            isMoving = true;
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, minHitPower));
        }
    }

	/*void OnMouseDown() {
		//Power of hit increases
		if (ball != null) {
			ball.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 0, maxHitPower));
		}
	}*/

	void OnMouseUp() {
		//Hit ball using power level.
	}

    void OnCollisionStay()
    {
        
    }
}
