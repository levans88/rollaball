using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Public variables will show up in the inspector
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    // Called on the first frame in which the script is active
    void Start() {
        // Retrieve the Rigidbody for phsics if it exists
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
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

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) {
            winText.text = "You Win!";
        }
    }
}