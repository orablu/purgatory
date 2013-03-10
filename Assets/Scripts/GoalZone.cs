/// Wes Rupert       - ora@outlook.com
/// Original Author  - Wes Rupert
/// Purgatory        - CameraTracking.cs
/// Script to control the goal detection and respose.

using UnityEngine;
using System.Collections;

public class GoalZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            GameObject.Find("_Game Manager").SendMessage("Goal");
        }
    }
}
