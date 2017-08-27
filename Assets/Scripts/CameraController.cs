using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float cameraSpeed = 10.0f;
    private Vector3 offset;

	void Start () {
        // Offset = distance between player and camera
        offset = transform.position - player.transform.position;
	}
	
    // Still runs once per frame but after all items have been processed in Update
    // When we set position of camera here, we know player has already moved for the frame.
	void LateUpdate () {
        // Move the camera with the player but maintain separation
        transform.position = player.transform.position + offset;

        // Allow mouse to control camera also
        // if (Input.GetAxis("Mouse X") > 0) {
        //     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSpeed);
            
        // }
        
        // if (Input.GetAxis("Mouse X") < 0) {
        //     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSpeed);
        // }
	}
}