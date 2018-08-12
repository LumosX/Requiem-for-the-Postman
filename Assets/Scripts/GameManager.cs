using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public RectTransform phoneUICanvas;
    public RectTransform phoneUIInfoPanel;
    public RectTransform phoneUIBossCall;
    public Text lblTime;
    public Text lblCorrect;
    public Text lblWrong;
    public Text lblBombAlert;
    public RectTransform panelGameOver;
    public Text lblLossReason;
    public Text lblGameOver;
    public Text lblItemInfo;
    public Text lblThirst;
    public Text lblLevel;

    public GameObject envelopePrefab;
    public Transform envelopeSpawnPoint;
    public GameObject voidTray;
    public GameObject teacup;
    public GameObject teacupPlate;
    public GameObject bulgarianDictionary;
    public GameObject germanDictionary;
    public GameObject norwegianDictionary;
    public GameObject russianDictionary;
    public GameObject chineseDictionary;

    public static GameManager instance;
    public static int CurLevel => instance.curLevel;


    public static LevelData[] levels = {
        new LevelData(3, 60, false, false, false, null),
        new LevelData(5, 70, false, false, true, null),
        new LevelData(5, 70, true, false, true, null),
        new LevelData(5, 80, true, false, true, Language.German),
        new LevelData(5, 110, true, false, true, Language.German, Language.Norwegian),
        new LevelData(10, 140, true, false, true, Language.German, Language.Norwegian, Language.Bulgarian),
        new LevelData(10, 160, true, false, true, Language.German, Language.Norwegian, Language.Bulgarian, Language.Russian),
        new LevelData(20, 200, true, false, true, Language.German, Language.Norwegian, Language.Bulgarian, Language.Russian),
        new LevelData(20, 300, true, false, true, Language.German, Language.Norwegian, Language.Bulgarian, Language.Russian, Language.Chinese),

    };

    private LevelData curLevelData;
    private int curLevel = 0;
    private int lettersCorrect = 0;
    private int lettersIncorrect = 0;
    private int targetLetters = 3;
    private float timeLeft = 60;

    private float thirst = 0;
    private float dehydrationRate = 100 / 60.0f; // you get thirsty in 1 minute

    private const int maxLettersWrong = 3;

    private int totalLettersCorrect;
    private int totalLettersWrong;
    private int totalBombsFound;
    private int totalBombsMissed;
    private int totalTeaSips;

    private bool isSpawningLetters = false;
    private float nextLetterSpawnTime = 0;
    private int numLettersInExistence = 0;
    private int maxLettersInExistence = 5;

    private bool playerLost = false;

    // At scene start: start from level 0, disable all unnecessary objects
	void Start () {
	    instance = this;
	    playerLost = false;

	    totalLettersCorrect = totalLettersWrong = totalBombsFound = totalBombsMissed = totalTeaSips = 0;
        panelGameOver.gameObject.SetActive(false);

	    StartLevel(8);
        //SpawnLetter();
	}
	
	// Update is called once per frame
	void Update () {
        // Tick-tock...
	    if (isSpawningLetters) {
	        timeLeft -= Time.deltaTime;
	        thirst += Time.deltaTime * dehydrationRate;
	    }
	    
        // Spawn letters
        if (isSpawningLetters && Time.time > nextLetterSpawnTime && numLettersInExistence <= maxLettersInExistence) SpawnLetter();


        // LOSE! if necessary
	    if (levels[curLevel].teacup && thirst >= 100) {
	        isSpawningLetters = false;
	        lblLossReason.text = "YOU DIED OF THIRST!";
	        panelGameOver.gameObject.SetActive(true);
	    }
	    else if (timeLeft <= 0 || lettersIncorrect >= maxLettersWrong) {
	        isSpawningLetters = false;
	        lblLossReason.text = "YOU'RE FIRED!";
            panelGameOver.gameObject.SetActive(true);
	    }

        // Also, PROGRESS TO THE NEXT LEVEL! if necessary
	    if (lettersCorrect >= targetLetters) {
            StartLevel(curLevel + 1);
	    }

	    UpdateThirstLevelUI();
	    UpdatePhoneUI();
	    UpdateGameOverUI();
	}

    void SpawnLetter() {
        var rand = new System.Random();

        // Pick parameters.
        // Dest is selected at random
        var letterDest = (Destination)(levels[curLevel].voidDests ? rand.Next(5) : rand.Next(4));
        // Bombs - if at all possible - have a very low probability: half of the current level, at a min of 6% and max of 35%
        var isBomb = rand.NextDouble() < Mathf.Clamp(curLevel / 2.0f, 0.06f, 0.35f);
        //if (!levels[curLevel].bombs) 
            isBomb = false; // TODO: Bombs are currently disabled. Maybe allow them if I have the time.
        // Language is selected randomly from those allowed
        var lang = levels[curLevel].languagesAllowed.ToList().GetRand();

        // Letters can be spawned with several rotations.
        var rotX = rand.Next(2) * 180; // i.e. 0 or 180 deg
        var rotY = rand.Next(2) * 180;
        var newLetter = Instantiate(envelopePrefab, envelopeSpawnPoint.position, Quaternion.Euler(rotX, rotY, 0));
        newLetter.GetComponent<Letter>().SetupLetter(curLevel, letterDest, isBomb, lang);

        // Delay letters a little bit as more dictionaries start piling up
        var maxLetterDelay = (curLevel < 4) ? 6 : 8;
        nextLetterSpawnTime = Time.time + (float)rand.NextDouble(4, maxLetterDelay);

        numLettersInExistence += 1;
    }




    public void StartLevel(int i) {
        // Generate infinite levels once we get past all of them
        if (i < levels.Length) {
            curLevelData = levels[i];
        }
        else {
            curLevelData.numLetters += 5;
            curLevelData.timeSecs += 40; // 8 seconds per letter
            // and that's it!
        }

        curLevel = i;

        voidTray.SetActive(levels[i].voidDests);
        // TODO bomb scanner
        teacup.SetActive(levels[i].teacup);
        teacupPlate.SetActive(levels[i].teacup);
        // dictionaries
        bulgarianDictionary?.SetActive(levels[i].languagesAllowed.Contains(Language.Bulgarian));
        germanDictionary?.SetActive(levels[i].languagesAllowed.Contains(Language.German));
        russianDictionary?.SetActive(levels[i].languagesAllowed.Contains(Language.Russian));
        norwegianDictionary?.SetActive(levels[i].languagesAllowed.Contains(Language.Norwegian));
        chineseDictionary?.SetActive(levels[i].languagesAllowed.Contains(Language.Chinese));

        timeLeft = levels[i].timeSecs;
        lettersCorrect = 0;
        lettersIncorrect = 0;
        targetLetters = levels[i].numLetters;

        // reset thirst only if we didn't care for it until now
        if (i == 0 || (!levels[i - 1].teacup && levels[i].teacup)) thirst = 0;

        numLettersInExistence = 0;
        isSpawningLetters = true;
    }

    void UpdateThirstLevelUI() {
        lblThirst.text = levels[curLevel].teacup ? $"Thirst: {thirst * 0.01:0%}" : "";

        lblLevel.text = "Level " + curLevel;
    }

    void UpdatePhoneUI() {
        var t = TimeSpan.FromSeconds(timeLeft);

        lblTime.text = $"{t.Minutes:D2}:{t.Seconds:D2}";
        lblCorrect.text = lettersCorrect + "/" + targetLetters;
        lblWrong.text = lettersIncorrect + "/" + maxLettersWrong;
    }

    private void UpdateGameOverUI() {
        lblGameOver.text =
            $"Survived until level {curLevel + 1}\n" + $"Total letters delivered: {totalLettersCorrect}\n" +
            $"Total letters misdelivered: {totalLettersWrong}\n" +
            $"Total sips of tea: {totalTeaSips}\n"; //+ $"Total bombs avoided: {totalBombsFound}\n" + $"Total bombs missed: {totalBombsMissed}";
    }


    public static void LetterDeliveredCorrectly(bool wasBomb) {
        instance.lettersCorrect += 1;
        instance.totalLettersCorrect += 1;
        instance.numLettersInExistence -= 1;

        if (wasBomb) instance.totalBombsFound += 1;
    }

    public static void LetterMistaken(bool wasBomb) {
        instance.lettersIncorrect += 1;
        instance.totalLettersWrong += 1;
        instance.numLettersInExistence -= 1;

        if (wasBomb) instance.totalBombsMissed += 1;
    }

    public static void HadASip() {
        instance.thirst = (instance.thirst > 60) ? instance.thirst - 60 : 0;
        instance.totalTeaSips += 1;
    }

    public static void UpdateHeldItemInfo(string info) {
        try {
            instance.lblItemInfo.text = info;
        }
        catch (Exception) {}
    }

    // this should be refactored, but I can't be arsed to deal with it right now
    public static string DestToStr(Destination val) {
        switch (val) {
            case Destination.LittleBarnton: return "Little Barnton";
            case Destination.Everbarrow: return "Everbarrow";
            case Destination.GreatHatchmill: return "Great Hatchmill";
            case Destination.GreatWaterset: return "Great Waterset";
            case Destination.Void: return "Redirect";
            default:
                throw new ArgumentOutOfRangeException(nameof(val), val, null);
        }
    }

}

public enum Destination {
    LittleBarnton,
    Everbarrow,
    GreatHatchmill,
    GreatWaterset,
    Void,
}

public enum Language {
    English = 1,
    German = 2,
    Bulgarian = 4,
    Norwegian = 8,
    Russian = 16,
    Chinese = 32
}

public class LevelData {
    public int numLetters = 3;
    public int timeSecs = 60;
    public bool voidDests = false;
    public bool bombs = false;
    public bool teacup = false;
    public HashSet<Language> languagesAllowed;

    public LevelData(int numLetters, int timeSecs, bool voidDests, bool bombs, bool teacup, params Language[] foreignLanguagesAllowed) {
        this.numLetters = numLetters;
        this.timeSecs = timeSecs;
        this.voidDests = voidDests;
        this.bombs = bombs;
        this.teacup = teacup;

        
        // Always allow english
        var langs = new HashSet<Language> {Language.English};
        if (foreignLanguagesAllowed != null) langs.UnionWith(foreignLanguagesAllowed);
        languagesAllowed = langs;
    }
}