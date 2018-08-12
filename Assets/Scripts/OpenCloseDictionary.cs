using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDictionary : MonoBehaviour {
    private Transform topHalf, bottomHalf;

    private bool open = false;
    private const float openSpeed = 120;
    private bool isDragged = false;

	// Use this for initialization
	void Start () {
	    topHalf = transform.Find("top half");
	    bottomHalf = transform.Find("bottom half");
	}

    void OnMouseDown() {
        isDragged = true;
    }

    void OnMouseUp() {
        isDragged = false;
    }

	// Update is called once per frame
	void Update () {
	    if (!topHalf || !bottomHalf) return;

        // if E is pressed, open of close the dictionary.
	    if (isDragged && Input.GetKeyDown(KeyCode.E)) open = !open;
        // open = TRUE => is closing = true; open = FALSE => must open = true;

        // "animate" to destination.
	    var targetZRot = topHalf.localEulerAngles.z + openSpeed * Time.deltaTime * (open ? -1 : 1);
	    targetZRot = Mathf.Clamp(targetZRot, 220, 359); // 360 - 140 = 220
        topHalf.localEulerAngles = new Vector3(topHalf.localEulerAngles.x, topHalf.localEulerAngles.y, targetZRot);
        //Debug.Log(open + " === " + targetZRot + " === " + topHalf.localEulerAngles.z);
	}
}
