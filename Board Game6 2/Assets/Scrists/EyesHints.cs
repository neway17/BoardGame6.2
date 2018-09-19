using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesHints : MonoBehaviour {
	public Text tapcalc;
	public Text eyescalc;
    public Text hintcalc;
    public Text hintcalc2;
	void Update() {
        hintcalc.text = PlayerPrefs.GetInt("Hints").ToString();
        hintcalc2.text = PlayerPrefs.GetInt("Hints").ToString();
		tapcalc.text = PlayerPrefs.GetInt("Taps").ToString();
		eyescalc.text = PlayerPrefs.GetInt("Eyes").ToString();
	}
    public void TapUsed()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            int i = PlayerPrefs.GetInt("Taps") - 1;
            PlayerPrefs.SetInt("Taps", i);
        }
    }
	public void EyeUsed() {
        if (PlayerPrefs.GetInt("Eyes") > 0 && PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            int i = PlayerPrefs.GetInt("Eyes") - 1;
            PlayerPrefs.SetInt("Eyes", i);
        }
	}
    public void HintUsed()
    {
        if (PlayerPrefs.GetInt("Hints") > 0 && PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            int i = PlayerPrefs.GetInt("Hints") - 1;
            PlayerPrefs.SetInt("Hints", i);
        }
    }
}
