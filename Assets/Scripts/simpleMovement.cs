using UnityEngine;
using System.Collections;

public class simpleMovement : MonoBehaviour {

    public float speed = 5f;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.left * -speed * Time.deltaTime);
        }
    }
}
