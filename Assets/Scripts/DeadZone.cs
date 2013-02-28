/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - DeadZone.cs
/// Script to control the actions of the dead zones.

using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Add animation.
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.SendMessage("Die");
        }
    }
}
