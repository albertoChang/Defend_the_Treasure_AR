using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool waveIsActive;

    // Use this for initialization
    void Start()
    {
        waveIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public static void killPlayer(Tower tower)
    {
        //Mostrar pantalla de derrota
        //Destroy(tower.gameObject);
        Debug.LogError("perdiste");
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Spawn()
    {
        this.GetComponent<WaveSpawner>().activateWave();
    }
}
