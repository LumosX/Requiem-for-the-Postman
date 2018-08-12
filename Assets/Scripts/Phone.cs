using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {

    public RectTransform phoneUICanvas;
    public RectTransform phoneUIInfoPanel;
    public RectTransform phoneUIBossCall;
    public Text lblBombAlert;
    public string itemInfo;

    //private bool isDragged = false;
    private bool onCallWithBoss = false;

    void Start() {
        OnMouseUp();
    }


    void OnMouseDown() {
        //isDragged = true;
        phoneUICanvas.gameObject.SetActive(true);

        phoneUIBossCall.gameObject.SetActive(false);
        phoneUIInfoPanel.gameObject.SetActive(true);

        GameManager.UpdateHeldItemInfo(itemInfo);
    }

    void OnMouseUp() {
        //isDragged = false;
        phoneUICanvas.gameObject.SetActive(false);
        GameManager.UpdateHeldItemInfo("");
    }

}
