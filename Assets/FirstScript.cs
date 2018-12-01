using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<LevelLoader>().SceneLoader(1);
        Debug.Log("Cargando....");
    }
	
}