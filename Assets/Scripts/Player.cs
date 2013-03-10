/// Wes Rupert       - ora@outlook.com
/// Original Author  - Andrew Heckman
/// Purgatory        - Player.cs
/// Script to control the player's actions.

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    // Player information.
    public static string playerName = "Player 1";
    public GameManager   manager;
    public Ghost         ghost;

    // Player physics.
	public float moveSpeed  = 5f;
	public float jumpForce = 300;
	public bool  canFlip    = true;
    public bool  isGrounded = true;
    public bool  isFlipping = false;

    // Player controls.
    public KeyCode leftKey  = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode jumpKey  = KeyCode.UpArrow;
    public KeyCode flipKey  = KeyCode.DownArrow;

    private Vector3 velocity;
	
    /// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start() {
	}
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
	void FixedUpdate() {
		if(manager.state == GameManager.GameState.LevelPlaying && !isFlipping) {
			if(Input.GetKey(leftKey)) {
                // Move left.
				transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
			}
			if(Input.GetKey(rightKey)) {
                // Move right.
				transform.Translate(Vector3.left * Time.deltaTime * -moveSpeed);
			}
			if(Input.GetKeyDown(jumpKey) & isGrounded) {
                // Jump.
				isGrounded = false;
				rigidbody.AddForce(0, jumpForce, 0);
			}
			if(Input.GetKeyDown(flipKey) & canFlip) {
                // Flip.
				flip();
			}
		}
	}
	
    /// <summary>
    /// Trigger for entering a collision. Grounds the player.
    /// </summary>
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Platform"){
			isGrounded = true;
		}
	}
	
    /// <summary>
    /// Trigger for exiting a collision. Ungrounds the player.
    /// </summary>
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Platform"){
			isGrounded = false;
		}
	}

    /// <summary>
    /// Flip the world. Freeze the player while flipping.
    /// </summary>
	void flip() {
        // Tell the world to flip.
		GameObject.Find("Camera").SendMessage("startFlipping");

        // Change flip-dependent values.
        if (jumpForce > 0 && Physics.gravity.y < 0 ||
            jumpForce < 0 && Physics.gravity.y > 0) {
            jumpForce = -jumpForce;
        }

        // Freeze the player.
        rigidbody.useGravity = false;
        velocity = rigidbody.velocity;
        rigidbody.velocity = Vector3.zero;
        velocity.y = -velocity.y;

        // Player can't flip now.
        canFlip = false;
        isFlipping = true;
	}

    /// <summary>
    /// Stop flipping. Unfreeze the player.
    /// </summary>
    public void stopFlipping() {
        // Flip the player.
        Vector3 position = transform.position;
		transform.position = ghost.transform.position;
        ghost.transform.position = position;

        // Unfreeze the player.
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;

        // Player can flip again.
        canFlip = true;
        isFlipping = false;
    }
	
    /// <summary>
    /// Tells the player to die.
    /// </summary>
	void Die(){
        // Tell the game manager that we've died.
        GameObject.Find("_Game Manager").SendMessage("Died");

        // Remove the player. TODO: Implement player as GameManager prefab object.
        // GameObject.Destroy(this);
	}
	
    /// <summary>
    /// Resets the character after death.
    /// </summary>
	void Respawn(Vector3 pos){
		transform.position = pos;
        canFlip = true;
        if (jumpForce < 0) {
            jumpForce = -jumpForce;
        }
	}
}

