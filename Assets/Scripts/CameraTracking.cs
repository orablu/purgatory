/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - CameraTracking.cs
/// Script to control the camera tracking and flip mechanic.

using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {
    private const float HALFTURN = 180f;

    /// <summary>
    /// The object that the camera is tracking.
    /// </summary>
    public GameObject trackedObject;

    /// <summary>
    /// True if the camera should follow behind the tracked object, otherwise snaps to it.
    /// </summary>
    public bool trackingDelay = true;

    /// <summary>
    /// How fast to fall bahind the player.
    /// </summary>
    public float trackingSpeed = 0.05f;

    /// <summary>
    /// How far max the camera can fall behind.
    /// </summary>
    public float maxTrackingDelay = 5f;

    /// <summary>
    /// The distance the camera stays from the scene.
    /// </summary>
    public float distance = 10f;

    /// <summary>
    /// The angle that the camera changes on each fixed update while moving.
    /// </summary>
    public float deltaAngle = 240f;

    /// <summary>
    /// Whether or not the camera is moving to the other side.
    /// </summary>
    private bool flipping = false;

    /// <summary>
    /// The total angle moved during movement.
    /// </summary>
    private float angleFlipped = 0f;

    /// <summary>
    /// Represents the status of the world.
    /// </summary>
    public enum Side { Norm, Flip };

    /// <summary>
    /// The present status of the world.
    /// </summary>
    public Side side {
        get;
        private set;
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start() {
        // Set up the camera in its initial position.
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, -distance);
        side = Side.Norm;
        flipping = false;

        // If we don't have a tracked object, find the player.
        if (trackedObject == null) {
            trackedObject = GameObject.Find("Player");
        }
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () {
        flip();
        track();
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval.
    /// </summary>
    void FixedUpdate() {
        // Nothing happening in here!
    }

    /// <summary>
    /// Updates the variables used to track movement.
    /// </summary>
    void startFlipping() {
        if (!flipping) {
            flipping = true;
            angleFlipped = 0f;
        }
    }

    /// <summary>
    /// Updates the variables used to track movement and st=naps the camera to its destination.
    /// </summary>
    void stopFlipping() {
        // Update status.
        side = (side == Side.Norm) ? Side.Flip : Side.Norm;
        flipping = false;
        
        // Reverse the gravity.
        Physics.gravity = Physics.gravity * -1f;

        // Tell the player to change, since we stopped flipping.
        GameObject.Find("Player").SendMessage("stopFlipping");
    }

    /// <summary>
    /// Flips the camera around the scene.
    /// </summary>
    void flip() {
        if (flipping) {
            // Flip 180 degrees.
            if (keepFlipping()) {
                transform.RotateAround(
                    new Vector3(transform.position.x, 0f, 0f),
                    Vector3.right,
                    deltaAngle * Time.deltaTime);
                angleFlipped += deltaAngle * Time.deltaTime;
            }
            else {
                stopFlipping();
            }
        }
        else {
            // Return to flat.
            float lastx = transform.position.x;
            if (transform.rotation.x > 0.5f) {
                // We're upside down.
                transform.rotation = new Quaternion(
                    1f,
                    transform.rotation.y,
                    transform.rotation.z,
                    0f);
                transform.position = new Vector3(lastx, 0f, distance);
            }
            else {
                // we're right side up.

                transform.rotation = new Quaternion(
                    0f,
                    transform.rotation.y,
                    transform.rotation.z,
                    1f);
                transform.position = new Vector3(lastx, 0f, -distance);
            }
        }
    }

    /// <summary>
    /// Tracks the tracked object.
    /// </summary>
    void track() {
        if (trackingDelay) {
            // Move faster the farther away the camera is from the tracked object.
            transform.Translate(
                Vector3.left
                * trackingSpeed
                * (transform.position.x - trackedObject.transform.position.x));

            // Clip the distance to the max tracking distance.
            transform.position = new Vector3(
                bound(transform.position.x, trackedObject.transform.position.x, maxTrackingDelay),
                transform.position.y,
                transform.position.z);
        }
        else {
            transform.Translate(
                Vector3.left
                * (transform.position.x - trackedObject.transform.position.x));
        }
    }

    /// <summary>
    /// Checks to see if the camera should continue flipping.
    /// <returns>True if the camera should continue flipping, otherwise false.</returns>
    /// </summary>
    bool keepFlipping() {
        if (side == Side.Norm) {
            return angleFlipped < HALFTURN
                && transform.rotation.x >= 0f
                && transform.rotation.w >= 0f;
        }
        else {
            return angleFlipped < HALFTURN
                && transform.rotation.x >= 0f
                && transform.rotation.w >= -1f;
        }
    }

    /// <summary>
    /// Limits the value to being within the given deviation from the desired value.
    /// </summary>
    /// <param name="value">The value to bound.</param>
    /// <param name="desired">The desired value to consider.</param>
    /// <param name="deviation">The allowed deviation from the desired value.</param>
    /// <returns>The bounded value.</returns>
    float bound(float value, float desired, float deviation) {
        if (value > desired + deviation) {
            return desired + deviation;
        }

        if (value < desired - deviation) {
            return desired - deviation;
        }

        return value;
    }
}
