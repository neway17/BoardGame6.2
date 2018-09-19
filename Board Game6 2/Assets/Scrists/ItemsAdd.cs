using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class ItemsAdd : MonoBehaviour {

    // Use this for initialization
    public void WatchAddForEye()
    {
        PlayerPrefs.SetString("Award", "Eye");
        AdsScripts.ShowRewardedvideo();
    }

    public void WatchAddForHint()
    {
        PlayerPrefs.SetString("Award", "Hint");
        AdsScripts.ShowRewardedvideo();
    }

    public void WatchAddForTap()
    {
        PlayerPrefs.SetString("Award", "Tap");
        AdsScripts.ShowRewardedvideo();
    }

    public void PurchaseManager_OnPurchaseConsumable(Product product)
    {
        if (product.definition.id == "addeye5") {
            PlayerPrefs.SetInt("Eyes", PlayerPrefs.GetInt("Eyes") + 5);
        }
        if (product.definition.id == "addeye15")
        {
            PlayerPrefs.SetInt("Eyes", PlayerPrefs.GetInt("Eyes") + 15);
        }
        if (product.definition.id == "addeye30")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 30);
        }
        if (product.definition.id == "addhint10")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 10);
        }
        if (product.definition.id == "addhint50")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 50);
        }
        if (product.definition.id == "addhint100")
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 100);
        }
        if (product.definition.id == "addtap20")
        {
            PlayerPrefs.SetInt("Taps", PlayerPrefs.GetInt("Taps") + 20);
        }
        if (product.definition.id == "addtap50")
        {
            PlayerPrefs.SetInt("Taps", PlayerPrefs.GetInt("Taps") + 50);
        }
        if (product.definition.id == "addtap100")
        {
            PlayerPrefs.SetInt("Taps", PlayerPrefs.GetInt("Taps") + 100);
        }
        
    }
}
