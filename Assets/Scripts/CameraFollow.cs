using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target = null;
    public float viewArea;
	private float yOffset;
	private float zOffset;
	private float rotateSpeed;
	private float xRotation;
	private float yRotation;
    private float minView;
    private float maxView;
    private float viewChangeAmount;
    public Vector3 ballDir;

    // Use this for initialization
    void Start () {
		yOffset = 2f;
		zOffset = -3f;
		rotateSpeed = 2.0f;

        minView = 75.0f;
        maxView = 125.0f;
        viewChangeAmount = 30.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			//Follow the target with the height and distance offsets.
			this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, target.transform.position.z + zOffset);

            //Allow rotation of camera based on mouse movement.
            if (Input.GetMouseButton(1))
            {
                xRotation += Input.GetAxis("Mouse X") * rotateSpeed;
                yRotation += Input.GetAxis("Mouse Y") * rotateSpeed;
            }

            transform.RotateAround(target.transform.position, Vector3.up, xRotation);
            transform.RotateAround(target.transform.position, Vector3.right, yRotation);

            //ballDir = Camera.main.transform.forward.normalized;
            //ballDir.y = 0.0f;
            //Debug.Log(ballDir);
            //Debug.DrawLine(target.transform.position, ballDir, Color.red, Mathf.Infinity);

            //Allow the camera to zoom in and out it's field of view using the scroll wheel.
            viewArea = Camera.main.fieldOfView;
            viewArea -= Input.GetAxis("Mouse ScrollWheel") * viewChangeAmount;
            viewArea = Mathf.Clamp(viewArea, minView, maxView);

            //Set the new view area angle to the main camera.
            Camera.main.fieldOfView = viewArea;

            //Orient camera toward the target.
            transform.LookAt(target.transform);
        }
	}
}
