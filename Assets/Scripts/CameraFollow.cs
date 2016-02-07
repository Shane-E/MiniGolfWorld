using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target = null;
	private float yOffset;
	private float zOffset;
	private float rotateSpeed;
	float xRotation;
	float yRotation;

	// Use this for initialization
	void Start () {
		yOffset = 2f;
		zOffset = -3f;
		rotateSpeed = 2.0f;

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

            //Orient camera toward the target.
            transform.LookAt(target.transform);
        }
	}
}
