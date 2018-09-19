using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public int packNum;
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
        int sum = 0;

        if (packNum > 0)
            for (int k = 1; k <= 30; k++)
                sum += PlayerPrefs.GetInt("" + (packNum * 30 - 30 + k) + "stars");
        else sum = 90;

        if (sum < 60)
            gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
