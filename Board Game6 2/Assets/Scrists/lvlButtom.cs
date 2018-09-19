using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlButtom : MonoBehaviour {
    public int lvl;
	// Use this for initialization
	void Start () {
        lvl += PlayerPrefs.GetInt("levelPack") * 30;
        if (!(PlayerPrefs.GetInt("" + (lvl - 1) + "stars") > 0))
            this.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
