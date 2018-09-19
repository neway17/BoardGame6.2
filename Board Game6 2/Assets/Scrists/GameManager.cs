using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public string lvl, lvlNum, packNum;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        lvl = PlayerPrefs.GetString("level");
        lvlNum = PlayerPrefs.GetInt("CurrentLevel").ToString();
        if (Input.GetKeyDown(KeyCode.Space))
            loadNext();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") -2);
            loadNext();
        }
    }

    public void loadNext()
    {
        StartCoroutine(waitAndLoad());
    }
    public IEnumerator waitAndLoad()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        if (PlayerPrefs.GetInt("CurrentLevel") != 121)
        {
            PlayerPrefs.SetString("level", PlayerPrefs.GetString(PlayerPrefs.GetInt("CurrentLevel").ToString()));
            if (PlayerPrefs.GetInt("toAdd") > 0)
                SceneManager.LoadScene(1);
            AdsScripts.LvlAdd();
        }
        else
        {
            //Congratulate
        }
    }
}
