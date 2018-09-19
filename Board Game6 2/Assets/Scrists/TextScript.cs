using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {
    public Text LvlTextFail;
	// Use this for initialization
	void Start () {
        string lvl = "" + PlayerPrefs.GetInt("CurrentLevel") % 30;
        if (lvl == "0")
            lvl = "30";
        
        LvlTextFail.text = "Level " + lvl + " failed";

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
