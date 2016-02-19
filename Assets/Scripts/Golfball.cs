using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Golfball : MonoBehaviour {
	public GameObject ball = null;
    public GameObject hole = null;
	public Text distance;
    public Slider powerbar;
	private bool isMoving = false;
	private bool increasing = true;
    private float distanceToHole;
	public float minHitPower = 40.0f;
    public float maxHitPower = 270.0f;
	private float hitPower = 0;
	private float powerIncrement = 5.0f;
	private float powerMultiplier = 10;
	private float ballRollTime = 0;

    // Use this for initialization
    void Start() {
        distance.GetComponent<Text>().text = "Distance To Hole:" + distanceToHole;
		ball.GetComponent<Rigidbody> ();
    }
	
	// Update is called once per frame
	void Update () {
        //Allow the ball to be hit if the ball is not null, not currently moving, and the left mouse button is clicked.
		if (ball != null) {
			if (Input.GetButton("Fire1") && !isMoving) {
				calculatePower ();
			}

            //Hit ball using power level and set ball to moving.
            if (Input.GetButtonUp("Fire1"))
            {
                hitBall(hitPower);
                isMoving = true;
            }

            //Debug.Log ("Ball Velocity: " + GetComponent<Rigidbody> ().velocity.magnitude);
            //Debug.Log (isMoving);

			//Detect when the ball stops
			if (isMoving) {
				ballRollTime += Time.deltaTime;
				if (ballRollTime > 1 && GetComponent<Rigidbody> ().velocity.magnitude <= 0.5) {
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
					isMoving = false;
				}
			} else {
				ballRollTime = 0;
			}

		}
        //Calculate distance to hole
        distanceToHole = Mathf.Round((Vector3.Distance(ball.transform.position, hole.transform.position) * 100f) / 100f);
        distance.GetComponent<Text>().text = "Distance To Hole: " + distanceToHole;
    }

	/*void OnMouseUp() {
		//Hit ball using power level and set ball to moving.
		Debug.Log ("Selected Power: " + hitPower);
		hitBall (hitPower);
		isMoving = true;
	}*/

	void calculatePower(){
        //Increase power if it is less than the max power.
        if (increasing)
        {
            if (hitPower < maxHitPower)
            {
                hitPower += powerIncrement * powerMultiplier;
                increasing = true;
            }
            else if (hitPower >= maxHitPower)
            {
                increasing = false;
            }
        }
		//Decrease power if power level is not increasing until the power hits the minimum level.
		if(!increasing) {
			//Debug.Log ("Not Increasing");
			if (hitPower > minHitPower) {
				//Debug.Log ("HitPower: " + hitPower);
				hitPower -= powerIncrement * powerMultiplier;
			} else if (hitPower <= minHitPower) {
				increasing = true;
			}
		}
		//Update the slider
		powerbar.value = hitPower / powerMultiplier;
	}

	void hitBall (float power){
		ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, power));

        //Reset the power  and power bar level to minimum default after hitting ball
        hitPower = minHitPower;
        powerbar.value = hitPower / powerMultiplier;
        Debug.Log("HitPower Reset: " + hitPower);
    }
}
