using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public GameObject textmeshPro;

    TextMeshProUGUI progressText;

    public void SceneLoader(int sceneIndex)
    {
        progressText = textmeshPro.GetComponent<TextMeshProUGUI>();

        FindObjectOfType<AudioManager>().Stop();

        StartCoroutine(LoadAsynchronously(sceneIndex));

        if (sceneIndex == 0)
            FindObjectOfType<AudioManager>().Play("Blood and Steel Loop");
        else if (sceneIndex == 1)
            FindObjectOfType<AudioManager>().Play("Loop 1 Tension");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = ((int)(progress * 100f)) + "%";
            yield return null;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("Loop 1 Tension");
    }
}
