using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour {
    TextMeshProUGUI scoreText;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Tu Puntaje Final : " + Score.scoreValue;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
