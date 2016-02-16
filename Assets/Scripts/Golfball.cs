using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
    public GameObject hole = null;
	public Text distance;
    public Slider powerbar;
	private bool isMoving = false;
    private float distanceToHole;
	public float minHitPower = 50.0f;
    public float maxHitPower = 275.0f;
	private float hitPower = 0;
	private float powerIncrement = 2.0f;
	private float powerMultiplier = 10;

    // Use this for initialization
    void Start() {
        distance.GetComponent<Text>().text = "Distance To Hole: " + distanceToHole;
    }
	
	// Update is called once per frame
	void Update () {
        //Allow the ball to be hit if the ball is not null, not currently moving, and the left mouse button is clicked.
		if (ball != null) {
			if (Input.GetButton("Fire1") && !isMoving) {
				if (hitPower >= maxHitPower) {
					hitPower -= powerIncrement * powerMultiplier;
				}else if (hitPower < maxHitPower) {
					hitPower += powerIncrement * powerMultiplier;
				}
                //Update the slider
                powerbar.value = hitPower / powerMultiplier;
				Debug.Log ("Power: " + hitPower);
			}

		}
        //Calculate distance to hole
        distanceToHole = Mathf.Round((Vector3.Distance(ball.transform.position, hole.transform.position) * 100f) / 100f);
        distance.GetComponent<Text>().text = "Distance To Hole: " + distanceToHole;
    }

	void OnMouseUp() {
		//Hit ball using power level.
		Debug.Log ("Selected Power: " + hitPower);
		hitBall (hitPower);
	}

	void hitBall (float power){
		ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, power));
	}
}
