using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Not using forces so we can use regular Update()
	void Update () {
        // Rotate the cube
        // Needs to be smooth (frame rate independent), so multiply by deltaTime
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
