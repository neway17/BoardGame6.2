using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JustRestart : MonoBehaviour {
    public GameObject EyeWAobj;
    public int qqq;
    public void Start()
    {
        qqq = PlayerPrefs.GetInt("EyesWA");
        if (PlayerPrefs.GetInt("EyesWA") == 2)
        {
            EyeWAobj.SetActive(true);
            PlayerPrefs.SetInt("EyesWA", 0);
        }
    }
	// Use this for initialization
	public void TryAgain() {
        PlayerPrefs.SetInt("EyesWA", PlayerPrefs.GetInt("EyesWA") + 1);
        SceneManager.LoadScene("Rubick's flat");
	}
	public void ExitMenu() {
		SceneManager.LoadScene ("Levels");
	}
}
