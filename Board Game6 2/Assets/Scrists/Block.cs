using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public bool tap = false;
    public bool light;
    public bool red = true;
    public float angle = 0, fingerAngle, deltaAngle;
    public float x, y;
    float distance;
    public bool hidden;
    public SpriteRenderer sprite;
    bool rotation = false;
    Color c;
    public Vector3 tapPos;
    int k = 0, anim = 0;
    bool updateRun = false;
    // Use this for initialization
    void Start () {
        //StartCoroutine(Hello());
        sprite = GetComponent<SpriteRenderer>();
        light = false;
        hidden = true;
        if (red)
            sprite.color = new Color(0.8f, 0, 0);
    }

   
    
    void Update() {
        paint();
        if (!rotation)
        {
            if (k == 0)
            {
                x = 0;
                y = 0;
            }

            if (k > 0)
            {
                k--;
                angle -= 6;
                transform.rotation = Quaternion.AngleAxis(k * 6, new Vector3(0, 0, 1));

                if (x != transform.position.x || y != transform.position.y)
                {
                    if (k > 0)
                        transform.position = new Vector3(cos(angle) * distance + x, sin(angle) * distance + y, -1);
                    else transform.position = new Vector3(cos(angle) * distance + x, sin(angle) * distance + y, 0);
                }
            }

            if (k < 0)
            {
                k++;
                angle += 6;
                transform.rotation = Quaternion.AngleAxis(k * 6, new Vector3(0, 0, 1));

                if (x != transform.position.x || y != transform.position.y)
                {
                    if (k < 0)
                        transform.position = new Vector3(cos(angle) * distance + x, sin(angle) * distance + y, -1);
                    else transform.position = new Vector3(cos(angle) * distance + x, sin(angle) * distance + y, 0);
                }
            }

            if (anim > 0)
                anim--;
            if (anim > 9)
                transform.Rotate(new Vector3(0, 0, 1), -5);
            if (anim > 3 && anim < 10)
                transform.Rotate(new Vector3(0, 0, 1), 5);
            if (anim > 0 && anim < 4)
                transform.Rotate(new Vector3(0, 0, 1), -5);
            if (anim == 1)
                transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            if (k > 4)
            {
                k--;                transform.RotateAround(new Vector3(x, y), new Vector3(0, 0, 1), deltaAngle);
            }
            else
            {
                tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                deltaAngle = Mathf.Atan((tapPos.y - y) / (tapPos.x - x)) / Mathf.PI * 180 - fingerAngle;

                if (deltaAngle > 100)
                    deltaAngle -= 180;
                if (deltaAngle < -100)
                    deltaAngle += 180;

                if (deltaAngle > 10)
                    deltaAngle = 10;
                if (deltaAngle < -10)
                    deltaAngle = -10;

                fingerAngle = Mathf.Atan((tapPos.y - y) / (tapPos.x - x)) / Mathf.PI * 180;
                transform.RotateAround(new Vector2(x, y), new Vector3(0, 0, 1), deltaAngle);
            }

            if (k == 5)
            {
                rotation = false;
                k = 0;
            }
        }
    }

    public void paint()
    {
        if (red)
            sprite.color = new Color(0.8f, 0, 0);
        else sprite.color = new Color(0.2f, 0.2f, 0.2f);

        if (hidden)
            hide();
        else show();

        if (light)
        {
            c = sprite.color;
            if (hidden)
                sprite.color = new Color(c.r, c.g, c.b, c.a - 0.15f);
            else sprite.color = new Color(c.r, c.g, c.b, c.a - 0.40f);
        }
    }

    public void hide()
    {
        c = sprite.color;
        sprite.color = new Color(c.r, c.g, c.b, 0.20f);
    }//+

    public void show()
    {
        c = sprite.color;
        sprite.color = new Color(c.r, c.g, c.b, 1);
    }//+

    public void startRotation(float x, float y)
    {
        this.x = x;
        this.y = y;
        rotation = true;
        tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerAngle = Mathf.Atan((tapPos.y - y) / (tapPos.x - x)) / Mathf.PI * 180;
        angle = Mathf.Atan((transform.position.y - y) / (transform.position.x - x)) / Mathf.PI * 180;

        if (transform.position.x == x && transform.position.y == y)
            angle = 0;
    }

    public void stopRotation()
    {
        k = 10;

        if (x != transform.position.x && y != transform.position.y)
        deltaAngle = (angle - Mathf.Atan((transform.position.y - y) / (transform.position.x - x)) / Mathf.PI * 180) % 90;

        if (deltaAngle > 45)
            deltaAngle -= 90;
        if (deltaAngle < -45)
            deltaAngle += 90;

        deltaAngle /= 5;
    }

    public void turnRight(float x, float y)
    {
        k = 15;
        this.x = x;
        this.y = y;
        distance = Mathf.Sqrt((transform.position.x - x) * (transform.position.x - x) + (transform.position.y - y) * (transform.position.y - y));
        angle = Mathf.Atan((transform.position.y - y) / (transform.position.x - x)) / Mathf.PI * 180;

        if (transform.position.x - x < 0)
            angle += 180;
    }//+

    public void turnLeft(float x, float y)
    {
        k =  -15;
        this.x = x;
        this.y = y;
        distance = Mathf.Sqrt((transform.position.x - x) * (transform.position.x - x) + (transform.position.y - y) * (transform.position.y - y));
        angle = Mathf.Atan((transform.position.y - y) / (transform.position.x - x)) / Mathf.PI * 180;

        if (transform.position.x - x < 0)
            angle += 180;
    }//+

    float sin(float i)
    {
        float angle = Mathf.PI / 180.0f * i;
        float sinAngle = Mathf.Sin(angle);
        return sinAngle;
    }//+
    float cos(float i)
    {
        float angle = Mathf.PI / 180.0f * i;
        float cosAngle = Mathf.Cos(angle);
        return cosAngle;
    }//+
    public void animation()
    {
        anim = 13;
    }
    void OnMouseDown()
    {
        tap = true;
    }//*/
}
