using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
        FindObjectOfType<PauseScript>().Resume();
    }
    
    public static void killPlayer(Tower tower,GameObject LosePanel)
    {
        Lose(LosePanel);
        FindObjectOfType<WaveSpawner>().DeActivateWave();
    }
    
    public void Spawn()
    {
        GetComponent<WaveSpawner>().activateWave();
    }

    public static void Won(GameObject WonPanel)
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("Bonus Theme 1 Tribal Victory");
        
        WonPanel.SetActive(true);
        FindObjectOfType<PauseScript>().Pause();
    }

    public static void Lose(GameObject LosePanel)
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("Bonus Theme 2 The Northmen March to War");

        LosePanel.SetActive(true);
        FindObjectOfType<PauseScript>().Pause();
    }
}
