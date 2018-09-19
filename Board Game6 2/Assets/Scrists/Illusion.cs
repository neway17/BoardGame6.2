using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Illusion : MonoBehaviour {
	public AudioSource auds;
    public int timer = 10;
    public static float updateTime = 0.013f;
    float scale;
    bool updateRun = false;
	// Use this for initialization
	void Start () {
        //StartCoroutine(Hello());
        Destroy(this.gameObject, 3);
        scale = transform.localScale.x * 0.1f;

        if (timer < 0)
            transform.localScale = new Vector3(0, 0);
    }
     

	

	// Update is called once per frame
	void Update () {
		if (timer == 10)
			auds.PlayOneShot (auds.clip);
			
        if (timer < 10 && timer > 0)
            transform.localScale = new Vector3(transform.localScale.x - scale, transform.localScale.y - scale);

        if (timer > -12 && timer < 0 && timer!= -1)
            transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y + scale);

        if (timer == 0)
            Destroy(this.gameObject);

        if (timer > 0)
            timer--;

        if (timer < -1)
            timer++;
	}
}
