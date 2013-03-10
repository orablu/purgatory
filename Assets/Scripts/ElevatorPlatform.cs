/// Wes Rupert       - ora@outlook.com
/// Original Author  - Wes Rupert
/// Purgatory        - CameraTracking.cs
/// Script for controlling elevator platforms.

using UnityEngine;
using System.Collections;

public class ElevatorPlatform : MonoBehaviour {

	public Vector3 upperBound;
	public Vector3 lowerBound;
	private bool reachedUpper = false;
	private bool reachedLower = false;
    public float speedX = 0.5f, speedY = 1.5f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void FixedUpdate() {
		float HorizontalIncrement = speedX*Time.fixedDeltaTime;
		float VerticalIncrement = speedY*Time.fixedDeltaTime;
		
		//left and right floating platform
		if((upperBound.x - lowerBound.x) != 0) {
			if(this.transform.position.x >= upperBound.x) {
					reachedUpper = true;
					reachedLower = false;
			}
			if(this.transform.position.x <= lowerBound.x) {
					reachedLower = true;
					reachedUpper = false;
			}
			if(reachedUpper == false) {
				transform.Translate(HorizontalIncrement, 0, 0);	
			} else if(reachedLower == false) {
				transform.Translate(-HorizontalIncrement, 0, 0);	
			}
		}
		
		
		//Up and Down floating platform
		if((upperBound.y - lowerBound.y) != 0) {
			if(this.transform.position.y >= upperBound.y) {
					reachedUpper = true;
					reachedLower = false;
			}
			if(this.transform.position.y <= lowerBound.y) {
					reachedLower = true;
					reachedUpper = false;
			}
			if(reachedUpper == false) {
				transform.Translate(0, VerticalIncrement, 0);	
			} else if(reachedLower == false) {
				transform.Translate(0, -VerticalIncrement, 0);	
			}
		}
	}
	
	
	
	
}
