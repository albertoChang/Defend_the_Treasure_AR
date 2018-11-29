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

        if (sceneIndex == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Bonus Theme 1 Tribal Victory");
            FindObjectOfType<AudioManager>().Stop("Bonus Theme 2 The Northmen March to War");
        }             
        else if (sceneIndex == 1)
            FindObjectOfType<AudioManager>().Stop("Blood and Steel Loop");

        StartCoroutine(LoadAsynchronously(sceneIndex));

        //FindObjectOfType<AudioManager>().Stop("At the Gates");

        if (sceneIndex == 0)
            FindObjectOfType<AudioManager>().Play("Blood and Steel Loop");
        else if (sceneIndex == 1)
            FindObjectOfType<AudioManager>().Play("Loop 1 Tension");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        //FindObjectOfType<AudioManager>().Play("At the Gates");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = ((int)(progress * 100f)) + "%";
            yield return null;
        }

        

        
    }
}
