using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour {
    Image image;
    public static float t = 1;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        Destroy(this.gameObject, 2);
        if (t == -1)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        else image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
	
	// Update is called once per frame
	void Update () {
        image.color = new Color(1, 1, 1, image.color.a + t * Time.deltaTime);
	}
}
