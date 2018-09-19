using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Button : MonoBehaviour {
    public bool tap = false;
    Vector3 tapPos;
    public int timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer--;
	}

    void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timer = 15;
        }
    }

    void OnMouseUp()
    {
        tapPos -= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (timer > 0 && tapPos.x + tapPos.y < 0.1f && tapPos.x + tapPos.y > -0.1f)
            tap = true;
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
