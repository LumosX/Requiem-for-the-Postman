using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnnoyingTicking : MonoBehaviour {

    public AudioClip[] tickTocks;
    private AudioSource source;
    
    private float nextPlayTime = 0;

    // We want to play these without interference
    void Start() {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }

    void Update() {
        if (source.isPlaying) return;
        if (Time.time < nextPlayTime) return;

        source.clip = tickTocks.ToList().GetRand();
        source.Play();
        nextPlayTime = Time.time + source.clip.length;
    }
}
