using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    public SpriteRenderer sprite;
    public Corner LD, LU, RD, RU;
    public float Size;
    // Use this for initialization
    void Start () {
        LD = Instantiate(LD, new Vector3(transform.position.x - Size * 0.5f + 0.25f, transform.position.y - Size * 0.5f + 0.25f), Quaternion.identity);
        LU = Instantiate(LU, new Vector3(transform.position.x - Size * 0.5f + 0.25f, transform.position.y + Size * 0.5f - 0.25f), Quaternion.identity);
        RD = Instantiate(RD, new Vector3(transform.position.x + Size * 0.5f - 0.25f, transform.position.y - Size * 0.5f + 0.25f), Quaternion.identity);
        RU = Instantiate(RU, new Vector3(transform.position.x + Size * 0.5f - 0.25f, transform.position.y + Size * 0.5f - 0.25f), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Delete()
    {
        Destroy(LU.gameObject);
        Destroy(RU.gameObject);
        Destroy(LD.gameObject);
        Destroy(RD.gameObject);
    }
}
