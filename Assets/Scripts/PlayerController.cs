using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Public variables will show up in the inspector
    public float rollSpeed;
    private Vector3 rollMovement;

    private float moveHorizontal;
    private float moveVertical;

    public float jumpForce;
    private Vector3 jumpMovement;

    public Text countText;
    public Text winText;
    
    private bool grounded = true;

    private Rigidbody rb;
    private int count;

    public Material defaultMaterial;
    public Material alternateMaterial;

    // Called on the first frame in which the script is active
    void Start() {
        // Retrieve the Rigidbody for phsics if it exists
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    
    // Using a Raycast to detect collision instead to limit to bottom of sphere
    //
    // void OnCollisionEnter(Collision collision) {
    //     foreach (ContactPoint contact in collision.contacts)
    //     {
    //         Debug.DrawRay(contact.point, contact.normal, Color.green, 20);
    //     }
        
    //     grounded = true;
    //     //GetComponent<MeshRenderer>().material = alternateMaterial;
    // }

	// Time to process frames can vary, accept input here
	void Update () {
        // Jumping

        // Get up and down directions ignoring the transform's rotation
        // (otherwise Transform.up would be used instead)
        Vector3 up = Vector3.up;    //0,1,0
        var down = up * -1;

        grounded = Physics.Raycast(transform.position, down, 0.75f);

        if (grounded == true && Input.GetButtonDown("Jump")) {
            //GetComponent<MeshRenderer>().material = defaultMaterial;
            grounded = false;

            // Set jump vector (jump height)
            jumpMovement = new Vector3(0, 2.0f, 0);
            
            // Make the ball jump
            rb.AddForce(jumpMovement * jumpForce, ForceMode.Impulse);
        }
    }

    // FixedUpdate has a regular timeline , apply physics here
    void FixedUpdate() {
        if (grounded == true) {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            
            // Create new vector from input
            rollMovement = new Vector3(moveHorizontal, 0, moveVertical);
            
            // Roll the ball
            rb.AddForce(rollMovement * rollSpeed);
        }
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