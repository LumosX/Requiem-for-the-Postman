using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {

    public AudioClip ring;
    public AudioClip hangUp;

    public RectTransform phoneUICanvas;
    public RectTransform phoneUIInfoPanel;
    public RectTransform phoneUIBossCall;
    public Text lblBombAlert;
    public string itemInfo;

    private bool isDragged = false;
    private bool onCallWithBoss = false;
    private bool isRinging = false;
    private float bossCallEndTime;

    private AudioSource source;
    private AudioClip callClip;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        OnMouseUp();

    }

    void Update() {
        if (onCallWithBoss && Time.time > bossCallEndTime) EndCall();

        if (onCallWithBoss && isDragged && Input.GetKeyDown(KeyCode.E)) {
            EndCall();
        }
    }

    public void StartRinging(AudioClip bossCallToQueue) {
        source.loop = true;
        source.clip = ring;
        source.volume = 0.9f;
        source.Play();

        // queue up clip for later
        callClip = bossCallToQueue;

        isRinging = true;
        phoneUICanvas.gameObject.SetActive(true);

        phoneUIBossCall.gameObject.SetActive(true);
        phoneUIInfoPanel.gameObject.SetActive(false);
    }

    public void EndCall() {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(hangUp, 0.7f);
        bossCallEndTime = Time.time;

        phoneUIBossCall.gameObject.SetActive(false);
        phoneUIInfoPanel.gameObject.SetActive(true);
        isRinging = false;
        onCallWithBoss = false;

        // turn the "screen" off if the call is over
        if (!isDragged) {
            phoneUICanvas.gameObject.SetActive(false);
        }
    }

    void OnMouseDown() {
        isDragged = true;

        // if ringing, start boss call
        if (isRinging) {
            isRinging = false;
            source.loop = false;
            source.Stop();
            source.clip = callClip;
            source.Play();

            bossCallEndTime = Time.time + callClip.length;
            onCallWithBoss = true;
        }
        // otherwise just do the things as normal
        else {
            phoneUICanvas.gameObject.SetActive(true);

            phoneUIBossCall.gameObject.SetActive(false);
            phoneUIInfoPanel.gameObject.SetActive(true);

            GameManager.UpdateHeldItemInfo(itemInfo);
        }
        
    }

    void OnMouseUp() {
        isDragged = false;

        GameManager.UpdateHeldItemInfo("");

        // keep the phone "on" if on call with the boss
        if (onCallWithBoss) return;
        phoneUICanvas.gameObject.SetActive(false);
    }

}
