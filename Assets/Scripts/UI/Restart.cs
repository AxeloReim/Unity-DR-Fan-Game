using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}

    public void Reload()
    {
        SceneManager.LoadScene("Base Level"); 
    }
}
