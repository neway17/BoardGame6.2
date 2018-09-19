using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratulationText : MonoBehaviour {
    public Text text;
    string[] s = new string[7];
	// Use this for initialization
	void Start () {
        s[0] = "WONDERFUL !";
        s[1] = "AMAZING !";
        s[2] = "WELL DONE !";
        s[3] = "VERY SMART !";
        s[3] = "JUST WOW !";
        s[4] = "INCREDIBLE !";
        s[5] = "ASTOUNDING !";
        s[6] = "BEWILDERING !";

        //xt = GetComponent<Text>();
        int k = Random.RandomRange(0, 6);
        text.text = s[k];
	}
	
	// Update is called once per frame
	void Update () {
    }
}
