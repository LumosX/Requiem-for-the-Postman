using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacup : MonoBehaviour {
    private float lastSipTime;
    public float sipCooldown = 5;
    private bool isDragged = false;

	// Use this for initialization
	void Start () {
	    lastSipTime = -20;
	}
	
    
    
    void OnMouseDown() {
        isDragged = true;
        GameManager.UpdateHeldItemInfo("Bring the cup close to you and press E to partially quench your thirst.");
    }

    void OnMouseUp() {
        isDragged = false;
        GameManager.UpdateHeldItemInfo("");
    }

    // Update is called once per frame
    void Update () {
        // if E is pressed, have a sip
        if (isDragged && Input.GetKeyDown(KeyCode.E) && Time.time > lastSipTime + sipCooldown) {
            // and it only works if the cup is close
            
            if (Camera.main.WorldToScreenPoint(transform.position).z > 19) return;
            //Debug.Log("HAD A SIP!");
            GameManager.HadASip();

            lastSipTime = Time.time;
        } 
    }
}
