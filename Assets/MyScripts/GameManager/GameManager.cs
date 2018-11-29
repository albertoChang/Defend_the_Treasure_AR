using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {

    }
    
    public static void killPlayer(Tower tower,GameObject LosePanel)
    {
        Lose(LosePanel);
    }

    public static void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
    }

    public static void Resume()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void Spawn()
    {
        this.GetComponent<WaveSpawner>().activateWave();
    }

    public static void Won(GameObject WonPanel)
    {
        FindObjectOfType<AudioManager>().Stop("Loop 1 Tension");
        FindObjectOfType<AudioManager>().Play("Bonus Theme 1 Tribal Victory");
        
        WonPanel.SetActive(true);
        
        Pause();
    }

    public static void Lose(GameObject LosePanel)
    {
        FindObjectOfType<AudioManager>().Stop("Loop 1 Tension");
        FindObjectOfType<AudioManager>().Play("Bonus Theme 2 The Northmen March to War");

        LosePanel.SetActive(true);
        
        Pause();
    }
}
