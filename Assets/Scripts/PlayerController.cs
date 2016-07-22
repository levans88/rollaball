using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Public variables will show up in the inspector
    public float speed;

    private Rigidbody rb;

    // Called on the first frame in which the script is active
    void Start() {
        // Retrieve the Rigidbody for phsics if it exists
        rb = GetComponent<Rigidbody>();
    }

	// Update has an irregular timeline (time to process frames can vary)
	// void Update () {}

    // FixedUpdate has a regular timeline (time to process each frame is always the same)
    // Use for physics
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create new vector from (x, y, z)
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }
}
