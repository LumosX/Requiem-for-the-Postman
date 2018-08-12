using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OutTray : MonoBehaviour {

    public AudioClip[] clips;
    public Destination dest;

    // Use this for initialization
    void Start() {
        transform.root.GetComponentInChildren<Text>().text = GameManager.DestToStr(dest).ToUpper();
    }

    void OnCollisionEnter(Collision collision) {
        // If colliding with an envelope, destroy the envelope.
        if (collision.gameObject.tag == "Envelope") {
            //Debug.Log("Out tray collided with an envelope");
            var letterData = collision.gameObject.GetComponent<Letter>();

            // Play a poofing sound as the letter disappears
            GetComponent<AudioSource>().PlayOneShot(clips.ToList().GetRand());

            // if it's a letter from a previous letter, just ignore it completely
            if (letterData.levelCreated != GameManager.CurLevel) {
                Destroy(collision.gameObject.transform.root.gameObject);
                return;
            }

            //Debug.Log("IS IT A BOMB: " + letterData.isBomb);

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
