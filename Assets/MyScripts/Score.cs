using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public static int scoreValue = 0;
    TextMeshProUGUI scoreText;

    void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
	}

    void Update()
    {
        //Debug.Log("Puntaje : " + scoreValue);
        scoreText.text = "Puntaje : " + scoreValue;
    }
}
