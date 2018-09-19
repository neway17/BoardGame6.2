using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class AdsScripts : MonoBehaviour {

	// Use this for initialization
	static void Start () {

	}

    public static void LvlAdd()
    {
        if (PlayerPrefs.GetInt("noAds") == 0)
        {
            if (PlayerPrefs.GetInt("toAdd") > 0)
                PlayerPrefs.SetInt("toAdd", PlayerPrefs.GetInt("toAdd") - 1);
            else
            {
                Debug.Log("akfgrv");
                PlayerPrefs.SetInt("toAdd", Random.Range(1, 3));
                Showvideo();
            }
        }
    }

    static public void Showvideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("video", options);
    }

    static public void ShowRewardedvideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResultRewVid;

        Advertisement.Show("rewardedVideo", options);
    }

    static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            SceneManager.LoadScene(1);
            Debug.Log("Video completed - Offer a reward to the player");
            // Reward your player here.
        }
        else if (result == ShowResult.Skipped)
        {
            SceneManager.LoadScene(1);
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
        }
        else if (result == ShowResult.Failed)
        {
            SceneManager.LoadScene(1);
            Debug.LogError("Video failed to show");
        }
    }

    static void HandleShowResultRewVid(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            if (PlayerPrefs.GetString("Award") == "Eye")
                PlayerPrefs.SetInt("Eyes", PlayerPrefs.GetInt("Eyes") + 1);
            if (PlayerPrefs.GetString("Award") == "Hint")
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 2);
            if (PlayerPrefs.GetString("Award") == "Tap")
                PlayerPrefs.SetInt("Taps", PlayerPrefs.GetInt("Taps") + 2);

            Debug.Log("Video completed - Offer a reward to the player");
            // Reward your player here.
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
