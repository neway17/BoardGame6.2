using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {
    public SpriteRenderer sprite;
    int i = 0;
    // Use this for initialization
    void Awake () {
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0.8f, 0, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
        i++;

        if (i > 0)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + 0.005f);
        else sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.005f);

        if (i >= 50)
        {
            i = -50;
        }
    }
}
