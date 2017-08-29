using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour {
	[HideInInspector]
	public Transform trackSegment;

	public Texture2D sourceImage;
	public int widthInTiles;
	


	// Use this for initialization
	void Start () {
		// Texture coordinates start at lower left corner
		Color32[] pixelsArray = sourceImage.GetPixels32();

		Debug.Log(pixelsArray);

		//foreach (var thing in pixelsArray) {
			//print("[" + thing.r + "]" + "[" + thing.g + "]" + "[" + thing.b + "]" );	
			//print(thing);
		//}

		//Instantiate(, new Vector3(0, 0, 0), Quaternion.identity);
	}
}
