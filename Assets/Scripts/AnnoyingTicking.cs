using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnnoyingTicking : MonoBehaviour {

    public AudioClip[] tickTocks;
    public AudioSource source1;
    //public AudioSource source2;

    private bool source1Next = true;
    private float nextPlayTime = 0;

    // We want to play these without interference
    void Start() {
        source1.loop = false;
        //source2.loop = false;

        // Play first clip initially
        //source1Next = true;

    }

    void Update() {
        if (source1.isPlaying) return;
        if (Time.time < nextPlayTime) return;

        source1.clip = tickTocks.ToList().GetRand();
        source1.Play();
        nextPlayTime = Time.time + source1.clip.length;
    }
}
