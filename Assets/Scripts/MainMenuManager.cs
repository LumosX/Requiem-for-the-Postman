using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    
    public GameObject envelopePrefab;
    public Transform envelopeSpawnPoint;

    private float nextLetterSpawnTime = 0;
    private int numLettersInExistence = 0;
    private int maxLettersInExistence = 100;
    
	
    // THIS IS A SHELL OF THE GameManager CLASS, INTENDED SOLELY TO SPAWN LETTERS IN THE MAIN MENU

	// Update is called once per frame
	void Update () {
	    
        // Spawn letters
        if (Time.time > nextLetterSpawnTime && numLettersInExistence <= maxLettersInExistence) SpawnLetter();

        
	}

    void SpawnLetter() {
        var rand = new System.Random();

        // Pick parameters.
        // Dest is selected at random
        var letterDest = (Destination)(GameManager.levels[0].voidDests ? rand.Next(5) : rand.Next(4));
        // Language is selected randomly from those allowed
        var lang = GameManager.levels[0].languagesAllowed.ToList().GetRand();

        // Letters can be spawned with several rotations.
        var rotX = rand.Next(2) * 180; // i.e. 0 or 180 deg
        var rotY = rand.Next(2) * 180;
        var newLetter = Instantiate(envelopePrefab, envelopeSpawnPoint.position, Quaternion.Euler(rotX, rotY, 0));
        newLetter.GetComponent<Letter>().SetupLetter(0, letterDest, false, lang);
        
        nextLetterSpawnTime = Time.time + (float)rand.NextDouble(4, 6);

        numLettersInExistence += 1;
    }

    public void BtnPlay_Click() {
        SceneManager.LoadScene("MainScene");
    }

    public void BtnQuit_Click() {
        Application.Quit();
    }
    
}
