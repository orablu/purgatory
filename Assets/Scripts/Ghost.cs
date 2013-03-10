/// Wes Rupert       - ora@outlook.com
/// Original Author  - Andrew Heckman
/// Purgatory        - Ghost.cs
/// Script to control the ghost's actions.

using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {
    public Player player;

    /// <summary>
	/// Use this for initialization.
    /// </summary>
	void Start () {
	}
	
    /// <summary>
	/// Update is called once per frame.
    /// </summary>
	void Update () {
        // Move to the player's location.
		Vector3 position = player.transform.position;
        position.y = -position.y;
		transform.position = position;
	}

    /// <summary>
    /// Trigger for entering a collision. Stops the player from flipping.
    /// </summary>
    void OnCollisionEnter() {
        setNoFlip();
	}

    /// <summary>
    /// Trigger for entering a collision. Stops the player from flipping.
    /// </summary>
    void OnCollisionStay() {
        setNoFlip();
    }
	
    /// <summary>
    /// Trigger for entering a collision. Reenables the player to flip.
    /// </summary>
    void OnCollisionExit() {
        setYesFlip();
	}
	
    /// <summary>
    /// Enables flipping.
    /// </summary>
    void setYesFlip() {
        player.canFlip = true;

        // TODO: Make ghost red when in collision.
    }

    /// <summary>
    /// Disables flipping.
    /// </summary>
    void setNoFlip() {
        player.canFlip = false;

        // TODO: Make ghost white when out of collision.
    }
}
