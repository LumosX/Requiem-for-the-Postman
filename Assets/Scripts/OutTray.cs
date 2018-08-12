using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutTray : MonoBehaviour {

    public Destination dest;

    // Use this for initialization
    void Start() {
        GetComponentInChildren<Text>().text = GameManager.DestToStr(dest).ToUpper();
    }

    void OnCollisionEnter(Collision collision) {
        // If colliding with an envelope, destroy the envelope.
        if (collision.gameObject.tag == "Envelope") {
            //Debug.Log("Out tray collided with an envelope");
            var letterData = collision.gameObject.GetComponent<Letter>();
            // bombs must be dropped into the void.
            if (letterData.isBomb) {
                GameManager.LetterMistaken(true);
            }
            else {
                if (letterData.dest == dest) GameManager.LetterDeliveredCorrectly(false);
                else GameManager.LetterMistaken(false);
            }



            Destroy(collision.gameObject.transform.root.gameObject);
        }
    }
}
