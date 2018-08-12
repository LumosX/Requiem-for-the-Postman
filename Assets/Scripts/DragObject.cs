using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class DragObject : MonoBehaviour {

    public bool resetOnDrop = false;

    private const float rotateSpeed = 200;
    private const float maxYOffset = 15;

    private Rigidbody rb;
    private Vector3 eulerAngles;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();

	    defaultPosition = transform.position;
	    defaultRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		// destroy or reset any draggable object if it falls into the void.
	    if (transform.position.y <= -200) {
	        if (gameObject.tag == "Envelope") {
                // if we drop an envelope and it's a bomb, all's well; if it's not a bomb, then it's bad
                if (GetComponent<Letter>().isBomb) GameManager.LetterDeliveredCorrectly(true);
                else GameManager.LetterMistaken(false);
	            Destroy(gameObject); // not elegant, but whatever
	            return;
	        }

            // if NOT an envelope, destroy or reset the object to its default position.
	        if (resetOnDrop) {
	            transform.position = defaultPosition + Vector3.up * 40; // make it drop from the sky just for fun
	            transform.rotation = defaultRotation;
	            GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
	        }
            else Destroy(gameObject);
	    }
	}

    void OnMouseDown() {
        // gotta keep a local copy of this to circumvent rotation issue.
        eulerAngles = transform.eulerAngles;
    }

    void OnMouseDrag()
    {
        // disable rigidbody whilst dragging to prevent gravitational acceleration
        rb.useGravity = false;

        //float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        var mousePosRatio = Mathf.InverseLerp(0, Screen.height, Input.mousePosition.y);
        var distance_to_screen = Mathf.Lerp(25, 50, mousePosRatio);
        var yOffset = Mathf.Lerp(maxYOffset, 0, mousePosRatio); // moving objects closer makes them "sink" lower

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen)) + Vector3.up * yOffset;
        //Debug.Log(mousePosRatio + " === " + distance_to_screen + " === " + transform.position.y);

        // allow objects to be rotated
        var pitch = Input.GetKey(KeyCode.W)
            ? -rotateSpeed
            : (Input.GetKey(KeyCode.S) ? rotateSpeed : 0);
        var roll = Input.GetKey(KeyCode.A)
            ? -rotateSpeed
            : (Input.GetKey(KeyCode.D) ? rotateSpeed : 0);
        var yaw = Input.GetKey(KeyCode.R)
            ? -rotateSpeed
            : (Input.GetKey(KeyCode.Q) ? rotateSpeed : 0);

        eulerAngles += new Vector3(pitch, yaw, roll) * Time.deltaTime;

        transform.eulerAngles = eulerAngles;
    }

    void OnMouseUp() {
        // when we're done dragging, enable gravity again
        rb.useGravity = true;
    }
}
