using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        // Camera ~= Transform
        // Offset = distance between player and camera
        offset = transform.position - player.transform.position;
	}
	
    // Still runs once per frame.
    // Guaranteed to run after all items have been processed in Update.
    // When we set position of camera here, we know player has already moved for the frame.
	void LateUpdate () {
        // Move the camera with the player but maintain their separation
        transform.position = player.transform.position + offset;
	}
}