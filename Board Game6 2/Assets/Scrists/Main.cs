using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class Main : MonoBehaviour
{
    public GameObject TapTut, StartHintButton, TapArrow;
    public GameObject[] stars;
    TimeSpan waitingTime = new TimeSpan(0, 0, 0, 120);
    public GameObject Zerotapsobj, tapAnimobj;
    public Animator ZeroTapsanim;
    public GameObject Zeroeyesobj;
    public Animator ZeroEyesanim;
    public Animator GameStartAnim;
    public AudioSource Audoff;
    public Text StartText;
    public float time;
    public Text TextMoves, tutText1, tutText2;
    public GameObject LooseObj;
    public GameObject GameOverobj;
    public Animator GameOverAnim;
    public float lastX, lastY;
    public Vector3 touch;
    public int moves;
    int star3 = 0, star2 = 0, star1 = 0;
    int hintNum = 0;
    public int solutionCounter;
    bool hintExist = false;
    public Hint hintPref;
    Hint hint;
    public Camera camera;
    public string s;
    public int k = 0;
    public float x = 0, y = 0;
    public Button button, resetButton;
    public Illusion illusion;
    public Block block;
    bool[,] matrix;
    Block[,] blocks;
    Button[,] buttons;
    //Field, Cube
    public int Fsize, Csize;
    public string answer, level;
    public Corner cBlock;
    public Text squareSize;
    bool rotation = false, updateRun = false;

    Vector3 pos;
	 void Awake()
	{
        Application.targetFrameRate = 55;

	}


	void awaking()
    {
        resetButton = Instantiate(button);
        resetButton.transform.localScale = new Vector3(100, 100);
        resetButton.transform.position = new Vector3(0, 0, 3);

        Illusion x;
        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                x = Instantiate(illusion, new Vector3(i + 1000, j + 1000), Quaternion.identity);
                x.GetComponent<SpriteRenderer>().color = blocks[i, j].GetComponent<SpriteRenderer>().color;
                x.timer = -((i + j) * 4 + 15);
            }

        }
        k = 1015 + Fsize * 8;//*/
    }
    void createLvl(string s)
    {
        answer = "";
        Fsize = s[0] - 48;
        Csize = s[1] - 48;

        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
            Csize = 4;

        matrix = new bool[Fsize, Fsize];
        blocks = new Block[Fsize, Fsize];

        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                matrix[i, j] = false;
                blocks[i, j] = Instantiate(block, new Vector3(i, j, 1), Quaternion.identity);
            }
        }
        char ch = s[2]; ;
        int o = 3;

        while (ch == '-')
        {
            matrix[s[o] - 48, s[o + 1] - 48] = true;
            o += 3;
            ch = s[o - 1];
        }//*/
        ch = s[o];

        if (ch == ';')
            o++;

        while (ch != ';')
        {
            blocks[s[o] - 48, s[o + 1] - 48].red = true;
            o += 3;
            ch = s[o - 1];
        }

        buttons = new Button[Fsize - Csize + 1, Fsize - Csize + 1];
        camera.transform.position = new Vector3(Fsize * 0.5f - 0.5f, Fsize * 0.5f - 0.5f - 0.4f, -3);
        camera.orthographicSize = (Fsize * 0.5f) / camera.aspect * 1.3f;

        for (int i = 0; i <= Fsize - Csize; i++)
            for (int j = 0; j <= Fsize - Csize; j++)
                buttons[i, j] = Instantiate(button, new Vector3(i + Csize * 0.5f - 0.5f, j + Csize * 0.5f - 0.5f, -2), Quaternion.identity);

        while (s[o] != ';')
        {
            answer += s[o];
            o++;
        }
        o++;

        while (s[o] != ';')
        {
            star1 = star1 * 10 + s[o] - 48;
            o++;
        }
        o++;

        while (s[o] != ';')
        {
            star2 = star2 * 10 + s[o] - 48;
            o++;
        }
        o++;

        while (s[o] != ';')
        {
            star3 = star3 * 10 + s[o] - 48;
            o++;
        }

        moves = star1;
        squareSize.text = "" + Csize + " x " + Csize;
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            Destroy(tutText1.gameObject);
            Destroy(tutText2.gameObject);
            Destroy(TapArrow.gameObject);
        }
        else StartHintButton.SetActive(false);

        cBlock = Instantiate(cBlock, new Vector3(-100, 0, -1), Quaternion.identity);
        cBlock.sprite.color = new Color(1, 1, 1, 0.2f);
        StartText.text = (PlayerPrefs.GetInt("CurrentLevel") % 30).ToString();

        if (StartText.text == "0")
            StartText.text = "30";

        time = Time.time;
        level = PlayerPrefs.GetString("level");
        createLvl(level);
        camera.transform.position = new Vector3(Fsize * 0.5f - 0.5f + 1000, Fsize * 0.5f - 0.5f + 1000 - 0.4f, -3);
        k = 1000000;
    }
    void Update()
    {
        if (cBlock.transform.position.x == -100)
        {
            TextMoves.text = moves.ToString();

            if (Time.time >= 0.4f + time && Time.time < 0.4f + Time.deltaTime + time)
            {
                check();
                awaking();
            }

            if (rotation)
            {
                checkTap();
                pos = blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f), Mathf.RoundToInt(y - Csize * 0.5f + 0.5f)].transform.position;
            }

            if (rotation && !(pos.y - y > 2  * (pos.x - x) && 2 * (pos.y - y) < pos.x - x))
            {
                bool redPoints = false;

                for (int i = 0; i < Csize; i++)
                {
                    for (int j = 0; j < Csize; j++)
                    {
                        if (blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].red)
                        {
                            pos.x = i;
                            pos.y = j;
                            redPoints = true;
                        }
                    }
                }

                if (redPoints)
                {
                    stopRotation();
                    blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f + pos.x), Mathf.RoundToInt(y - Csize * 0.5f + 0.5f + pos.y)].animation();
                }
            }//*/

            if (k == 1)
            {
                touch = new Vector3(0, 0);
                resetButton.tap = false;

                for (int i = 0; i <= Fsize - Csize; i++)
                    for (int j = 0; j <= Fsize - Csize; j++)
                        buttons[i, j].tap = false;
            }

            if (k == 0 && !rotation)
            {
                if (Input.GetMouseButtonDown(0) && !rotation && x * y != 0)
                    startRotation();

                if (Input.GetMouseButtonDown(0))
                    touch = camera.ScreenToWorldPoint(Input.mousePosition);

                check();
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    turnRight();

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    turnLeft();

                if (Input.GetKeyDown(KeyCode.Space))
                    reset();

                if (Input.GetKeyDown(KeyCode.Escape))
                    restart();

                checkTap();

                int t = fullNum();
                if (t == 0)
                    endGame(true);
                else
                    if (t > 0 && t < Fsize || moves == 0)
                    endGame(false);
            }


            if (k > 0)
                k--;

            if (k % 65 == 0 && k > 65 && k < 1000)
            {
                if (x != 0 && y != 0)
                    check();
                recreationNextStep();
            }

            if (k == 57)
                restart();

            if (k == 1001)
                check();

            if (k == 1000)
            {
                k = 0;
                camera.transform.position = new Vector3(Fsize * 0.5f - 0.5f, Fsize * 0.5f - 0.5f - 0.4f, -3);

                if (PlayerPrefs.GetInt("CurrentLevel") == 4)
                {
                    TapTut.SetActive(true);
                }
                //VVSHint();
            }

            if (k == 2000)
            {
                camera.transform.position = new Vector3(Fsize * 0.5f - 0.5f - 100, Fsize * 0.5f - 0.5f - 100 - 0.4f, -3);
            }

            if (Input.GetMouseButtonUp(0) && rotation && k == 0 && x * y != 0)
                stopRotation();
        }
        else
        {
            for (int i = 0; i < Fsize; i++)
            {
                for (int j = 0; j < Fsize; j++)
                {
                    if (blocks[i, j].tap && (PlayerPrefs.GetInt("CurrentLevel") != 4 || (i == 3 && j == 3) || (i == 1 && j ==1)))
                    {
                        blocks[i, j].tap = false;
                        if (matrix[i, j] && !blocks[i, j].red && cBlock.transform.position != new Vector3(i, j, -2))
                        {
                            if (PlayerPrefs.GetInt("CurrentLevel") == 4)
                            {
                                tutText1.GetComponent<Animator>().Play("delete");
                                tutText2.GetComponent<Animator>().Play("TutorialText");
                            }
                            tapAnimobj.GetComponent<Animator>().Play("tapbad");
                            cBlock.transform.position = new Vector3(i, j, -2);
                        }

                        if (cBlock.transform.position.x != -50 && !matrix[i, j])
                        {
                            if (PlayerPrefs.GetInt("CurrentLevel") == 4)
                                tutText2.GetComponent<Animator>().Play("delete");

                            PlayerPrefs.SetInt("Taps", PlayerPrefs.GetInt("Taps") - 1);
                            matrix[Mathf.CeilToInt(cBlock.transform.position.x), Mathf.CeilToInt(cBlock.transform.position.y)] = false;
                            matrix[i, j] = true;
                            boost();
                        }
                    }
                }
            }
        }
    }
    void stopRotation()
    {
        if (x * y != 0)
        {
            int rotationLevel = 0;
            Vector3 pos = blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f), Mathf.RoundToInt(y - Csize * 0.5f + 0.5f)].transform.position;
            if (pos.x - x > 0 && pos.y - y > 0)
                rotationLevel = 2;

            if (pos.x - x > 0 && pos.y - y < 0)
                rotationLevel = 3;

            if (pos.x - x < 0 && pos.y - y < 0)
                rotationLevel = 0;

            if (pos.x - x < 0 && pos.y - y > 0)
                rotationLevel = 1;

            for (int i = 0; i < Csize; i++)
            {
                for (int j = 0; j < Csize; j++)
                {
                    blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].stopRotation();
                }
            }

            if (Csize % 2 == 1)
            {
                blocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].deltaAngle = blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f), Mathf.RoundToInt(y - Csize * 0.5f + 0.5f)].deltaAngle;
            }
            rotation = false;
            k = 6;

            bool[,] m = new bool[Csize, Csize];

            for (int o = 0; o < rotationLevel; o++)
            {
                for (int i = 0; i < Csize; i++)
                {
                    for (int j = 0; j < Csize; j++)
                    {
                        m[i, j] = matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j];
                    }
                }

                for (int i = 0; i < Csize; i++)
                {
                    for (int j = 0; j < Csize; j++)
                    {
                        matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j] = m[Csize - 1 - j, i];
                    }
                }
            }

            if (rotationLevel > 0 && (lastY != y || lastX != x))
            {
                moves--;
                lastX = x;
                lastY = y;
            }

            for (int i = 0; i <= Fsize - Csize; i++)
            {
                for (int j = 0; j <= Fsize - Csize; j++)
                {
                    buttons[i, j].tap = false;
                }
            }
            resetButton.tap = false;
        }
    }
    void startRotation()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != 4 && !IsPointerOverUIObject())
        {
            if (StartHintButton.active)
                StartHintButton.GetComponent<Animator>().Play("restart+hint");

            if (hintExist)
            {
                hint.Delete();
                Destroy(hint.gameObject);
                hintExist = false;
            }

            rotation = true;
            for (int i = 0; i < Csize; i++)
            {
                for (int j = 0; j < Csize; j++)
                {
                    blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].startRotation(x, y);
                }
            }
        }
    }
    void check()
    {
        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                if (matrix[i, j])
                    blocks[i, j].hidden = false;
                else blocks[i, j].hidden = true;
                blocks[i, j].transform.position = new Vector3(i, j, -1);
                blocks[i, j].paint();
            }
        }

        bool[,] lines = new bool[2, Fsize];
        for (int i = 0; i < Fsize; i++)
        {
            bool k = true;

            for (int j = 0; j < Fsize; j++)
            {
                k = k && matrix[i, j];
            }

            lines[0, i] = k;
            k = true;

            for (int j = 0; j < Fsize; j++)
            {
                k = k && matrix[j, i];
            }
            lines[1, i] = k;
        }

        for (int i = 0; i < Fsize; i++)
        {
            if (lines[0, i])
            {
                for (int j = 0; j < Fsize; j++)
                {
                    Illusion x = Instantiate(illusion, new Vector3(i, j), Quaternion.identity);
                    x.GetComponent<SpriteRenderer>().color = blocks[i, j].GetComponent<SpriteRenderer>().color;
                    x.timer = 1 + 5 * j;
                    blocks[i, j].red = false;
                    matrix[i, j] = false;
                    blocks[i, j].hidden = true;
                    blocks[i, j].paint();
                }
                //k = 11 + 5 * Fsize;
            }

            if (lines[1, i])
            {
                for (int j = 0; j < Fsize; j++)
                {
                    Illusion x = Instantiate(illusion, new Vector3(j, i), Quaternion.identity);
                    x.GetComponent<SpriteRenderer>().color = blocks[j, i].GetComponent<SpriteRenderer>().color;
                    x.timer = 1 + 5 * j;
                    blocks[j, i].red = false;
                    matrix[j, i] = false;
                    blocks[j, i].hidden = true;
                    blocks[j, i].paint();
                }
                //k = 5 * Fsize;
            }
        }//*/
    }//+
    public void turnRight()
    {
        if ((k == 0 && touch.y != 0) || solutionCounter > 0)
            if (touch.y > y)
            {
                if (x != 0)
                {
                    bool redPoints = false;

                    for (int i = 0; i < Csize; i++)
                    {
                        for (int j = 0; j < Csize; j++)
                        {
                            if (blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].red)
                            {
                                blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].animation();
                                redPoints = true;
                            }
                        }
                    }

                    if (!redPoints)
                    {
                        if (lastX != x || lastY != y)
                        {
                            moves--;
                            lastX = x;
                            lastY = y;
                        }

                        if (hintExist)
                        {
                            hint.Delete();
                            Destroy(hint.gameObject);
                        }
                        bool[,] m = new bool[Csize, Csize];

                        for (int i = 0; i < Csize; i++)
                        {
                            for (int j = 0; j < Csize; j++)
                            {
                                m[i, j] = matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j];
                            }
                        }

                        for (int i = 0; i < Csize; i++)
                        {
                            for (int j = 0; j < Csize; j++)
                            {
                                blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].turnRight(x, y);
                                matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j] = m[Csize - 1 - j, i];
                            }
                        }

                        if (k < 25)
                            k = 26;
                        hintExist = false;
                    }
                    else k = 1;
                }
                //touch = new Vector3(0, 0);
            }
            else
            {
                touch.y = 100000;
                turnLeft();
            }
    }//+
    public void turnLeft()
    {
        if ((k == 0 && touch.y != 0) || solutionCounter > 0)
            if (touch.y > y)
            {
                if (x != 0)
                {
                    bool redPoints = false;

                    for (int i = 0; i < Csize; i++)
                    {
                        for (int j = 0; j < Csize; j++)
                        {
                            if (blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].red)
                            {
                                blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].animation();
                                redPoints = true;
                            }
                        }
                    }

                    if (!redPoints)
                    {
                        if (lastX != x || lastY != y)
                        {
                            moves--;
                            lastX = x;
                            lastY = y;
                        }

                        if (hintExist)
                        {
                            hint.Delete();
                            Destroy(hint.gameObject);
                        }
                        bool[,] m = new bool[Csize, Csize];

                        for (int i = 0; i < Csize; i++)
                        {
                            for (int j = 0; j < Csize; j++)
                            {
                                m[i, j] = matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j];
                            }
                        }

                        for (int i = 0; i < Csize; i++)
                        {
                            for (int j = 0; j < Csize; j++)
                            {
                                blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].turnLeft(x, y);
                                matrix[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j] = m[j, Csize - 1 - i];
                            }
                        }

                        if (k < 26)
                            k = 25;
                        hintExist = false;
                    }
                    else k = 1;
                }
                touch = new Vector3(0, 0);
            }
            else
            {
                touch.y = 100000;
                turnRight();
            }
    }//+
    public void Up()
    {
        touch.y = 100000;
        if (touch.x > x)
            turnLeft();
        else turnRight();
    }
    public void Down()
    {
        touch.y = 100000;
        if (touch.x < x)
            turnLeft();
        else turnRight();
    }
    void select()
    {
        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                blocks[i, j].light = true;
            }
        }

        for (int i = 0; i < Csize; i++)
        {
            for (int j = 0; j < Csize; j++)
            {
                blocks[Mathf.RoundToInt(x - Csize * 0.5f + 0.5f) + i, Mathf.RoundToInt(y - Csize * 0.5f + 0.5f) + j].light = false;
            }
        }
    }//+
    void checkTap()
    {
        for (int i = 0; i <= Fsize - Csize; i++)
        {
            for (int j = 0; j <= Fsize - Csize; j++)
            {
                if (buttons[i, j].tap)
                {
                    if (rotation)
                    {
                        stopRotation();
                        k = 0;
                    }

                    buttons[i, j].tap = false;

                    if (x == buttons[i, j].transform.position.x && y == buttons[i, j].transform.position.y)
                        reset();
                    else
                    {
                        x = buttons[i, j].transform.position.x;
                        y = buttons[i, j].transform.position.y;
                        select();
                    }
                }
            }
        }
        if (resetButton.tap)
        {
            stopRotation();
            resetButton.tap = false;
            reset();
        }
    }//+
    public void reset()
    {
        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                blocks[i, j].light = false;
            }
        }

        for (int i = 0; i < Fsize - Csize; i++)
        {
            for (int j = 0; j < Fsize - Csize; j++)
            {
                buttons[i, j].timer = 0;
                buttons[i, j].tap = false;
            }
        }

        x = 0;
        y = 0;
    }//+
    int fullNum()
    {
        int k = 0;

        for (int i = 0; i < Fsize; i++)
            for (int j = 0; j < Fsize; j++)
                if (matrix[i, j])
                    k++;

        return k;
    }
    public void restart()
    {
        for (int i = 0; i < Fsize; i++)
            for (int j = 0; j < Fsize; j++)
                matrix[i, j] = false;

        if (hintExist)
        {
            hint.Delete();
            Destroy(hint.gameObject);
        }//*/

        hintExist = false;
        char ch = level[2]; ;
        int o = 3;

        while (ch == '-')
        {
            matrix[level[o] - 48, level[o + 1] - 48] = true;
            o += 3;
            ch = level[o - 1];
        }//*/
        ch = level[o];

        while (ch != ';')
        {
            blocks[level[o] - 48, level[o + 1] - 48].red = true;
            blocks[level[o] - 48, level[o + 1] - 48].sprite.color = new Color(0.5f, 0, 0);
            o += 3;
            ch = level[o - 1];
        }
        k = 0;
        check();

        lastX = 0;
        lastY = 0;

        moves = star1;
        reset();
    }
    public void useHint()
    {
        if (PlayerPrefs.GetInt("Hints") > 0)
        {
            reset();
            hintNum++;
            if (hintNum == 1)
                useHint1();
            else useHint2();

            lastX = 0;
            lastY = 0;
        }
        else
        {
            // окошко закончились подсакзки hint
        }
    }
    public void useHint1()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            restart();
            hint = Instantiate(hintPref, new Vector3(answer[0] - 48 + Csize * 0.5f - 0.5f, answer[1] - 48 + Csize * 0.5f - 0.5f, 2), Quaternion.identity);
            hint.Size = Csize;
            hintExist = true;
        }
    }
    public void useHint2()
    {
        restart();
        int k = 2;

        while (answer[k] != 'L' && answer[k] != 'R')
            k += 3;

        x = answer[k - 2] - 48 + Csize * 0.5f - 0.5f;
        y = answer[k - 1] - 48 + Csize * 0.5f - 0.5f;
        select();

        if (answer[2] == 'L')
            turnLeft();

        if (answer[2] == 'R')
            turnRight();
        reset();

        k += 3;

        while (answer[k] != 'L' && answer[k] != 'R')
            k += 3;

        hint = Instantiate(hintPref, new Vector3(answer[k - 2] - 48 + Csize * 0.5f - 0.5f, answer[k - 1] - 48 + Csize * 0.5f - 0.5f, 2), Quaternion.identity);
        hint.Size = Csize;
        hintExist = true;
    }
    public void recreate()
    {
        bool rot = rotation;
        if (rotation)
            stopRotation();

        if (PlayerPrefs.GetInt("CurrentLevel") != 4)
        {
            if (PlayerPrefs.GetInt("Eyes") > 0)
            {
                if (k == 0 || rot)
                {
                    if (hintExist)
                    {
                        hint.Delete();
                        Destroy(hint.gameObject);
                    }

                    reset();
                    solutionCounter = (answer.Length + 1) / 3;
                    k = 65 * solutionCounter + 100;

                    for (int i = 0; i < Fsize; i++)
                    {
                        for (int j = 0; j < Fsize; j++)
                        {
                            matrix[i, j] = false;
                            blocks[i, j].hidden = true;
                            blocks[i, j].red = false;
                        }
                    }
                }
            }
            else
            {
                Zeroeyesobj.SetActive(true);
                ZeroEyesanim.Play("zeroeyes");
            }
        }
        else
        {
            TapTut.SetActive(true);
        }
    }
    void recreationNextStep()
    {
        touch = new Vector3(0, 0);
        if (answer[solutionCounter * 3 - 1] == 'L')
        {
            x = answer[solutionCounter * 3 - 3] - 48 + Csize * 0.5f - 0.5f;
            y = answer[solutionCounter * 3 - 2] - 48 + Csize * 0.5f - 0.5f;
            turnRight();
        }

        if (answer[solutionCounter * 3 - 1] == 'R')
        {
            x = answer[solutionCounter * 3 - 3] - 48 + Csize * 0.5f - 0.5f;
            y = answer[solutionCounter * 3 - 2] - 48 + Csize * 0.5f - 0.5f;
            turnLeft();
        }

        if (answer[solutionCounter * 3 - 1] == 'S')
        {
            x = 0;
            y = 0;

            for (int i = 0; i < Fsize; i++)
            {
                if (answer[solutionCounter * 3 - 3] == 'x')
                {
                    matrix[answer[solutionCounter * 3 - 2] - 48, i] = true;
                    blocks[answer[solutionCounter * 3 - 2] - 48, i].hidden = false;
                }
                else
                {
                    matrix[i, answer[solutionCounter * 3 - 2] - 48] = true;
                    blocks[i, answer[solutionCounter * 3 - 2] - 48].hidden = false;
                }
            }
        }

        solutionCounter--;
    }
    int starsCount()
    {
        if (star1 - moves <= star3)
            return 3;

        if (star1 - moves <= star2)
            return 2;
        else
            return 1;
    }
    public void endGame(bool win)
    {
        GameStartAnim.SetBool("BoolOver", true);
        k = -1;
        if (win)
            PlayerPrefs.SetInt(PlayerPrefs.GetInt("CurrentLevel").ToString() + "stars", Mathf.Max(PlayerPrefs.GetInt(PlayerPrefs.GetInt("CurrentLevel").ToString() + "stars"), starsCount()));
        
        Illusion x;
        for (int i = 0; i < Fsize; i++)
        {
            for (int j = 0; j < Fsize; j++)
            {
                x = Instantiate(illusion, new Vector3(i, j), Quaternion.identity);
                x.GetComponent<SpriteRenderer>().color = blocks[i, j].GetComponent<SpriteRenderer>().color;
                x.timer = (i + j) * 6;
                Destroy(blocks[i, j].gameObject);
            }
        }

        if (win)
        {
            StartCoroutine(spawnGameOverObj());
        }
        else
        {
            LooseObj.SetActive(true);
        }
    }
    IEnumerator spawnGameOverObj()
    {
        yield return new WaitForSeconds(1);
        GameOverobj.SetActive(true);

        if (starsCount() == 1)
        {
            GameOverAnim.SetInteger("GameOverStartNumber", 1);
            Destroy(stars[1].gameObject);
            Destroy(stars[2].gameObject);
        }
        if (starsCount() == 2)
        {
            GameOverAnim.SetInteger("GameOverStartNumber", 2);
            Destroy(stars[2].gameObject);
        }
        if (starsCount() == 3)
        {
            GameOverAnim.SetInteger("GameOverStartNumber", 3);
        }
    }
    public DateTime convertToDate(string s)
    {
        int i = 0, mon = 0, day = 0, year = 0, sec = 0, hour = 0, min = 0;


        while (s[i] != '/')
        {
            mon = mon * 10 + s[i] - 48;
            i++;
        }
        i++;

        while (s[i] != '/')
        {
            day = day * 10 + s[i] - 48;
            i++;
        }
        i++;

        while (s[i] != ' ')
        {
            year = year * 10 + s[i] - 48;
            i++;
        }
        i++;

        while (s[i] != ':')
        {
            hour = hour * 10 + s[i] - 48;
            i++;
        }
        i++;

        while (s[i] != ':')
        {
            min = min * 10 + s[i] - 48;
            i++;
        }
        i++;

        while (s[i] != ' ')
        {
            sec = sec * 10 + s[i] - 48;
            i++;
        }
        i++;

        if (s[i] == 'A')
            return new DateTime(year, mon, day, hour, min, sec);
        else return new DateTime(year, mon, day, hour + 12, min, sec);
    }
    public void boost()
    {
        if (rotation)
            stopRotation();

        if (PlayerPrefs.GetInt("Taps") > 0 || cBlock.transform.position.x != -100)
        {
            reset();
            if (cBlock.transform.position.x == -100)
            {
                if (PlayerPrefs.GetInt("CurrentLevel") == 4)
                {
                    tutText1.GetComponent<Animator>().Play("TutorialText");
                    Destroy(TapArrow.gameObject);
                }

                tapAnimobj.GetComponent<Animator>().Play("tap");
                cBlock.transform.position = new Vector3(-50, 0, -1);
                for (int i = 0; i <= Fsize - Csize; i++)
                    for (int j = 0; j <= Fsize - Csize; j++)
                        buttons[i, j].transform.position = new Vector3(-100, 0, -2);

                for (int i = 0; i < Fsize; i++)
                    for (int j = 0; j < Fsize; j++)
                        blocks[i, j].tap = false;
            }
            else
            {
                tapAnimobj.GetComponent<Animator>().Play("tapbad");
                cBlock.transform.position = new Vector3(-100, 0, -1);
                for (int i = 0; i <= Fsize - Csize; i++)
                    for (int j = 0; j <= Fsize - Csize; j++)
                        buttons[i, j].transform.position = new Vector3(i + Csize * 0.5f - 0.5f, j + Csize * 0.5f - 0.5f, -2);
            }
        }
        else
        {
            // Кончились тапы
            Zerotapsobj.SetActive(true);
            ZeroTapsanim.Play("zerotaps");
        }
    }
    public void removeGameOverObj()
    {
        GameOverobj.GetComponent<Animator>().Play("nextsc");
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