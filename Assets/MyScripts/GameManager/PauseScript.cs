using UnityEngine;

public class PauseScript : MonoBehaviour {

    public void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
    }

    public void Resume()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
}
