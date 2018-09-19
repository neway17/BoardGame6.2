using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{

    string[] levels;
    public int packNum;
    // Use this for initialization
    void Awake()
    {
        packNum = PlayerPrefs.GetInt("levelPack");
    }

    void Start()
    {
        PlayerPrefs.SetInt("0stars", 3);
        //startLevel(6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startLevel(int i)
    {

        if (PlayerPrefs.GetInt("1stars") == 0)
            SceneManager.LoadScene(3);
        else
        {
            if (i == 1 || PlayerPrefs.GetInt("" + (i + packNum * 30 - 1) + "stars") > 0)
            {
                PlayerPrefs.SetInt("CurrentLevel", (i + packNum * 30));
                PlayerPrefs.SetString("level", PlayerPrefs.GetString("" + (i + packNum * 30)));
                SceneManager.LoadScene(1);
            }
        }
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}
