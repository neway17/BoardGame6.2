using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starsToUnlolockText : MonoBehaviour {
    public int packNum;
	// Use this for initialization
	void Start () {
        int k = 0;

        for (int i = 1; i <= packNum * 30 - 30; i++)
            k += PlayerPrefs.GetInt("" + i + "stars");

        if (packNum * 60 - k - 60 > 0)
            GetComponent<Text>().text = "" + (packNum * 60 - k - 60) + " stars to unlock";
        else GetComponent<Text>().text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
