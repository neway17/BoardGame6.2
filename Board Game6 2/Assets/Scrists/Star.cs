using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
    public int lvl;
    public int number;
	// Use this for initialization
	void Start () {
        lvl += PlayerPrefs.GetInt("levelPack") * 30;
        if (PlayerPrefs.GetInt("" + lvl + "stars") < number)
            this.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
