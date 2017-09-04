using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour {
	public Transform trackSegment;
	public Texture2D sourceImage;
	public int widthInSegments = 8;
	public int heightInSegments = 8;
	public float gap = .05f;

	private int srcWidth;
	private int srcHeight;
	private int[,] pixelArrayBW;
	private int[,] segments;
	private int[,] line;
	private float[,] slopes;

	// Use this for initialization
	void Start () {
		srcWidth = sourceImage.width;
		srcHeight = sourceImage.height;

		pixelArrayBW = new int[srcWidth,srcHeight];
		
		simplifyColorArray();
		buildSegmentsArray();
		placeSegments();
	}

	// Read pixels of texture
	// Convert from flat array of Color32[] to 2d array like int[,]
	private void simplifyColorArray() {
		
		// The returned array is a flattened 2D array, where pixels are laid out left to right, bottom to top (i.e. row after row). Texture coordinates start at lower left corner.
		Color32[] pixelArrayColor = sourceImage.GetPixels32();

		//Debug.Log(pixelsArray);	    //UnityEngine.Color32[]
		//Debug.Log(pixelsArray[0]);	//RGBA(255,255,255,255)
		//Debug.Log(pixelsArray[0].a);	//255
		//Debug.Log(pixelsArray[0].b);	//255		
		var coloredPixelCount = 0;
		var c = 0; // Value to read from pixelArrayColor flat array
		var y = 0; // Row to start writing new columns in
		while (y < srcHeight) {
			var x = 0; // Column position to write to
			while (x < srcWidth) {
				// If the pixel at position c is not white...
				if (pixelArrayColor[c].r < 255 || pixelArrayColor[c].g < 255 || pixelArrayColor[c].b < 255) {
					pixelArrayBW[x,y] = 1;
					coloredPixelCount++;
				}
				else {
					pixelArrayBW[x,y] = 0;
				}
				x++; 
				c++; 
			}
			y++;
		}
		
		// Debugging
		Debug.Log("Colored pixel count from pixelArrayColor[]: " + coloredPixelCount);
		// var itemString = "";
		// var i = 0;
		// foreach (var item in pixelArrayBW) {
		// 	itemString += i + ": " + item + ", ";
		// 	i++;
		// }
		// Debug.Log(itemString);
	}

	// Convert pixelArrayBW to array of tiles with user specified width and height
	private void buildSegmentsArray() {
		segments = new int[widthInSegments,heightInSegments];

		// Test - Store slopes 
		slopes = new float[widthInSegments,heightInSegments];
		int[,] line = new int[2,2];

		var segmentCount = 0;

		// Chunks are like rows and columns
		// Determine chunk size and quantity, round down to stay in bounds
		var chunkSizeY = (int)Math.Floor(srcHeight/(decimal)heightInSegments);
		var chunkQuantityY = heightInSegments;
		var chunkIndexY = 0;
		var y = 0;
		
		// For each Y chunk (row)...
		while (chunkIndexY < chunkQuantityY) {
			var chunkSizeX = (int)Math.Floor(srcWidth/(decimal)widthInSegments);
			var chunkQuantityX = widthInSegments;
			var chunkIndexX = 0;
			var x = 0;

			// For each X chunk (column) in the current Y chunk (row)
			while (chunkIndexX < chunkQuantityX){
				var coloredPixelCount = 0;

				// While x is within the current chunk...
				while (x >= chunkIndexX * chunkSizeX && x < (chunkIndexX * chunkSizeX) + chunkSizeX) {
					var foundFirst = false;
					//var foundLast = false;
					
					// If the value at x,y is 1, count it as a black pixel
					if (pixelArrayBW[x,y] == 1) {
						coloredPixelCount++;

						// Test - Store last black pixel coordinates found
						 int[,] last = new int[1,2];
						 last[0,0] = x;
						 last[0,1] = y;

						// Test - Find lowest (first) pixel in chunk
						if (foundFirst == false) {
							foundFirst = true;
							line[0,0] = x;
							line[0,1] = y;
						}

						// Test - Find the highest (last) pixel in chunk
						// If we're on the last pixel in the chunk
						if (x == ((chunkIndexX * chunkSizeX) + chunkSizeX) - 1) {
							//if (foundLast == false) {
								//foundLast = true;
								Debug.Log("Last: " + last[0,0] + ", " + last[0,1]);
								line[1,0] = last[0,0];
								line[1,1] = last[0,1];
							//}
						}
					}
					x++;
				}

				// If there is a colored pixel in the chunk there is a segment here
				if (coloredPixelCount > 0) {
					// Write segment to segments array
					segments[chunkIndexX,chunkIndexY] = 1;
					segmentCount++;

					// Test - Use first and last pixels to get chunk slope
					// Slope = "change in Y" / "change in X"
					// Convert slope to angle
					// Write chunk slope to slopes array
					float deltaY = line[1,1] - line[0,1];
					float deltaX = line[1,0] - line[0,0];
					//float slope = deltaY / deltaX;
					
					// SOHCAHTOA...
					//Get inverse tangent of O/A and convert from rad to deg

					if (deltaY == 0 && deltaX == 0) {
						
					}
					
					if (deltaY == 0 && deltaX !=0) {

					}

					if (deltaY != 0 && deltaX == 0) {

					}

					if 

					var angleZrad = (Mathf.Atan(deltaY / deltaX));
					var angleZ = angleZrad * Mathf.Rad2Deg;
					//Debug.Log(angleZrad);
					//Debug.Log(angleZ);

					slopes[chunkIndexX,chunkIndexY] = angleZ;
				}
				else {
					// Write there is NO segment to segments array
					segments[chunkIndexX,chunkIndexY] = 0;
				}

				chunkIndexX += 1; // Go to the next X chunk
			}
			
			chunkIndexY += 1; // Go to the next Y chunk

			// Increase y in pixelArrayBW by chunkSizeY to start at next row
			y += chunkSizeY;
		}

		Debug.Log("Segment count from segments[,]: " + segmentCount);
	}

	public void placeSegments() {
		for (var y = 0; y <= segments.GetUpperBound(1); y++) {
			for (var x = 0; x <= segments.GetUpperBound(0); x++) {
				//Debug.Log(x.ToString() + ", " + y.ToString() + " = " + segments[x,y]);
				if (segments[x,y] == 1) {
					var offsetX = 0;
					var offsetY = 0;

					// pos = offset + ((array[x] + un-zero-index) * (segment width + gap)
					var xpos = offsetX + ((x + 1) * (trackSegment.localScale.x + gap));

					var ypos = offsetY + ((y + 1) * (trackSegment.localScale.x + gap));

					Instantiate(trackSegment, new Vector3(xpos, ypos, 0), Quaternion.identity).Rotate(0,0,slopes[x,y]);
				}
			}
		}
	}
}
