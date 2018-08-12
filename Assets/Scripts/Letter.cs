using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour {

    public int levelCreated; // easier than pooling letters and destroying them when levels end.
    public Destination dest;
    public bool isBomb;
    public Language language = Language.English;
    public string itemInfo;

    public void SetupLetter(int level, Destination dest, bool isBomb, Language lang) {
        this.levelCreated = level;
        this.isBomb = isBomb;
        this.dest = dest;

        GetComponentInChildren<Text>().text =
            GenerationUtilities.GenerateName(lang) + "\n" + GenerationUtilities.GenerateAddress(lang) + "\n" + GenerationUtilities.GenerateDestName(lang, dest);

        itemInfo = "Drop this letter into the " + (lang == Language.English ? "\"" + GameManager.DestToStr(dest) + "\"" : "correct") +
                   " box";
    }

    void OnMouseDown() {
        GameManager.UpdateHeldItemInfo(itemInfo);
    }

    void OnMouseUp() {
        GameManager.UpdateHeldItemInfo("");
    }
}
