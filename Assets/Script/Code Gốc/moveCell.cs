using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class moveCell : MonoBehaviour
{
    public Transform cell;
    static public Vector3[,] arrPos = new Vector3[10, 15];
    private bool[,] arrkt = new bool[10, 15];
    private bool[,] arrMarkTag = new bool[10, 15];
    private float x = 0, y = 0;
    private float x1 = 0, y1 = 0;
    private int xRes = 5;
    private int yRes = 5;
    public float fSince = 0;
    static public bool isPress = false;
    static public bool isChuyen = false;
    bool isChange = false;
    private float fWidCell;
    private float fHeiCell;
    int xi, yj;
    float fTime = 0;
    static public bool isRun = false;
    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (!PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.SetFloat("Speed", 1);
        }
        Time.timeScale = PlayerPrefs.GetFloat("Speed");
        if (!PlayerPrefs.HasKey("Best1"))
        {
            PlayerPrefs.SetInt("Best1", 0);
        }
        if (!PlayerPrefs.HasKey("Best2"))
        {
            PlayerPrefs.SetInt("Best2", 0);
        }
        if (!PlayerPrefs.HasKey("Best3"))
        {
            PlayerPrefs.SetInt("Best3", 0);
        }
        if (!PlayerPrefs.HasKey("Score1"))
        {
            PlayerPrefs.SetInt("Score1", 0);
        }
        if (!PlayerPrefs.HasKey("Score2"))
        {
            PlayerPrefs.SetInt("Score2", 0);
        }
        if (!PlayerPrefs.HasKey("Score3"))
        {
            PlayerPrefs.SetInt("Score3", 0);
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (!PlayerPrefs.HasKey("Style"))
        {
            PlayerPrefs.SetInt("Style", 1);
        }

    }

    void Start()
    {
        fWidCell = cell.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        fHeiCell = cell.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        isChuyen = false;
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            PlayerPrefs.SetInt("R", 0);
            PlayerPrefs.SetInt("RT", 0);
            PlayerPrefs.SetInt("RB", 0);
            PlayerPrefs.SetInt("L", 0);
            PlayerPrefs.SetInt("LT", 0);
            PlayerPrefs.SetInt("LB", 0);
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            PlayerPrefs.SetInt("R", 1);
            PlayerPrefs.SetInt("RT", 0);
            PlayerPrefs.SetInt("RB", 0);
            PlayerPrefs.SetInt("L", 1);
            PlayerPrefs.SetInt("LT", 0);
            PlayerPrefs.SetInt("LB", 0);
        }
        else
        {
            PlayerPrefs.SetInt("R", 1);
            PlayerPrefs.SetInt("RT", 0);
            PlayerPrefs.SetInt("RB", 0);
            PlayerPrefs.SetInt("L", 0);
            PlayerPrefs.SetInt("LT", 1);
            PlayerPrefs.SetInt("LB", 1);
        }
        for (int k = 1; k < 4; k++)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
                {
                    if (!PlayerPrefs.HasKey(i + "." + j + k))
                    {
                        PlayerPrefs.SetInt(i + "." + j + k, 0);
                    }
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
            {
                arrPos[i, j] = new Vector3(0 - fWidCell / 4.6f * (4 - j), fSince - fHeiCell / 3.1f * (i - 2), 0);
                arrkt[i, j] = true;
                arrMarkTag[i, j] = true;
                PlayerPrefs.SetInt(i + "." + j + PlayerPrefs.GetInt("Level") + ".i", i);
                PlayerPrefs.SetInt(i + "." + j + PlayerPrefs.GetInt("Level") + ".j", j);
            }
        }
        if (PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("Level")) == 0)
        {
            //ktDeath.isDie = false;
            for (int i = 0; i < 2; i++)
            {
                xi = Random.Range(0, 5);
                yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                while (xi % 2 != yj % 2)
                {
                    yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                }
                while (PlayerPrefs.GetInt(xi + "." + yj + PlayerPrefs.GetInt("Level")) > 0)
                {
                    xi = Random.Range(0, 5);
                    while (xi % 2 != yj % 2 || !PlayerPrefs.HasKey(xi + "." + yj + PlayerPrefs.GetInt("Level")))
                    {
                        yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                    }
                }
                arrPos[xi, yj] = new Vector3(0 - fWidCell / 4.6f * (4 - yj), fSince - fHeiCell / 3.1f * (xi - 2), 0);
                int e = Random.Range(0, 9);
                if (e < 6)
                {
                    e = 1;
                }
                else
                    e = 2;
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level"), e);
                Transform trans1 = Instantiate(cell, arrPos[xi, yj], Quaternion.identity) as Transform;
                trans1.tag = xi + "." + yj;
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level") + ".i", xi);
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level") + ".j", yj);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
                {
                    if (PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")) > 0)
                    {
                        Transform trans1 = Instantiate(cell, arrPos[i, j], Quaternion.identity) as Transform;
                        trans1.tag = i + "." + j;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!ktDeath.isDie)
        if(true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Nhan chuot");
                x = Input.mousePosition.x;
                y = Input.mousePosition.y;
            }
            if (PlayerPrefs.GetInt("Win" + PlayerPrefs.GetInt("Level")) == 1)
            {
                x = Input.mousePosition.x;
                y = Input.mousePosition.y;
            }
            if (Input.GetMouseButton(0))
            {
                x1 = Input.mousePosition.x;
                y1 = Input.mousePosition.y;

            }
            if (Input.GetMouseButtonUp(0))
            {
                isPress = true;
            }
            //if (!Setting.isSet)
            if(true)
            {
                if (Time.time - fTime > 0.2f)
                {
                    if (Input.GetKeyDown(KeyCode.D) && PlayerPrefs.GetInt("R") == 0)
                    {
                        xRes = 0;
                        yRes = 2;
                        isRun = true;
                    }
                    if (PlayerPrefs.GetInt("L") == 0)
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            xRes = 0;
                            yRes = -2;
                            isRun = true;
                        }
                    if (Input.GetKeyDown(KeyCode.W) && PlayerPrefs.GetInt("LT") == 0)
                    {
                        xRes = -1;
                        yRes = -1;
                        isRun = true;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && PlayerPrefs.GetInt("RT") == 0)
                    {
                        xRes = -1;
                        yRes = 1;
                        isRun = true;
                    }
                    if (Input.GetKeyDown(KeyCode.X) && PlayerPrefs.GetInt("RB") == 0)
                    {
                        xRes = 1;
                        yRes = 1;
                        isRun = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Z) && PlayerPrefs.GetInt("LB") == 0)
                    {
                        xRes = 1;
                        yRes = -1;
                        isRun = true;
                    }

                    if (isPress && Mathf.Abs(x1 - x) + Mathf.Abs(y1 - y) > 30)
                    {
                        Vector2 vec = new Vector2(x1 - x, y1 - y);
                        vec.Normalize();
                        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

                        if (30 >= angle && angle >= -30 && PlayerPrefs.GetInt("R") == 0)
                        {
                            xRes = 0;
                            yRes = 2;
                            isRun = true;
                        }
                        if (PlayerPrefs.GetInt("L") == 0)
                            if (150 <= angle || angle <= -150)
                            {
                                xRes = 0;
                                yRes = -2;
                                isRun = true;
                            }
                        if (150 >= angle && angle >= 90 && PlayerPrefs.GetInt("LT") == 0)
                        {
                            xRes = -1;
                            yRes = -1;
                            isRun = true;
                        }
                        if (90 >= angle && angle >= 30 && PlayerPrefs.GetInt("RT") == 0)
                        {
                            xRes = -1;
                            yRes = 1;
                            isRun = true;
                        }
                        if (-90 <= angle && angle <= -30 && PlayerPrefs.GetInt("RB") == 0)
                        {
                            xRes = 1;
                            yRes = 1;
                            isRun = true;
                        }
                        if (-150 <= angle && angle <= -90 && PlayerPrefs.GetInt("LB") == 0)
                        {
                            xRes = 1;
                            yRes = -1;
                            isRun = true;
                        }
                    }
                    if (isPress)
                    {
                        isPress = false;
                    }
                }
                //								print ("Angle:" + angle);
                //								print ("Xres:" + xRes + " yREs:" + yRes);
                if (isRun && Time.time - fTime > 0.2f)
                {
                    isChuyen = false;
                    isRun = false;
                    int n = 4;
                    int m = 8;

                    int dn = -1;
                    int dm = -2;
                    if (xRes < 0)
                    {
                        n = 0;
                        dn = 1;
                    }
                    if (yRes < 0)
                    {
                        dm = 2;
                    }
                    while (0 <= n && n <= 4)
                    {
                        if (yRes < 0)
                        {
                            m = Mathf.Abs(2 - n);
                        }
                        else
                        {
                            m = 9 - Mathf.Abs(2 - n) - 1;
                        }
                        while (Mathf.Abs(2 - n) <= m && m < 9 - Mathf.Abs(2 - n))
                        {
                            if (PlayerPrefs.HasKey(n + "." + m + PlayerPrefs.GetInt("Level")) && PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level")) > 0)
                            {
                                int i = PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".i");
                                int j = PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".j");
                                bool ktchuyen = false;
                                while (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                {
                                    if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) > 0)
                                    {
                                        if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level")) && arrkt[(i + xRes), (j + yRes)])
                                        {
                                            i += xRes;
                                            j += yRes;
                                            arrkt[i, j] = false;

                                            ktchuyen = true;
                                        }
                                        break;

                                    }
                                    else if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0)
                                    {
                                        i += xRes;
                                        j += yRes;

                                        ktchuyen = true;
                                    }
                                }
                                if (!arrkt[i, j])
                                {
                                    PlayerPrefs.SetInt(i + "." + j + PlayerPrefs.GetInt("Level"), PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level")) + 1);
                                    if (PlayerPrefs.GetInt("Win" + PlayerPrefs.GetInt("Level")) == 0 && PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")) == 11)
                                    {
                                        PlayerPrefs.SetInt("Win" + PlayerPrefs.GetInt("Level"), 1);
                                    }
                                }
                                else
                                    PlayerPrefs.SetInt(i + "." + j + PlayerPrefs.GetInt("Level"), PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level")));
                                if (ktchuyen)
                                {
                                    PlayerPrefs.SetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".i", i);
                                    PlayerPrefs.SetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".j", j);
                                    PlayerPrefs.SetInt(n + "." + m + PlayerPrefs.GetInt("Level"), 0);
                                }
                            }

                            m += dm;
                        }
                        n += dn;
                    }

                    n = 4;
                    m = 8;
                    if (xRes < 0)
                    {
                        n = 0;
                    }
                    while (0 <= n && n <= 4)
                    {
                        if (yRes < 0)
                        {
                            m = Mathf.Abs(2 - n);
                        }
                        else
                        {
                            m = 9 - Mathf.Abs(2 - n) - 1;
                        }
                        while (Mathf.Abs(2 - n) <= m && m < 9 - Mathf.Abs(2 - n))
                        {
                            arrMarkTag[n, m] = true;
                            arrkt[n, m] = true;
                            if (PlayerPrefs.HasKey(n + "." + m + PlayerPrefs.GetInt("Level")))
                            {
                                int i = PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".i");
                                int j = PlayerPrefs.GetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".j");

                                if (i != n || j != m)
                                {
                                    isChuyen = true;
                                    Transform trans;
                                    if (GameObject.FindGameObjectWithTag(n + "." + m) == null)
                                    {
                                        //																				print (n + "." + m);
                                    }
                                    else
                                    {
                                        trans = GameObject.FindGameObjectWithTag(n + "." + m).transform;
                                        trans.tag = i + "." + j;
                                    }
                                }
                            }
                            PlayerPrefs.SetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".i", n);
                            PlayerPrefs.SetInt(n + "." + m + PlayerPrefs.GetInt("Level") + ".j", m);
                            m += dm;
                        }
                        n += dn;
                    }
                    if (isChuyen)
                    {
                        fTime = Time.time;
                    }
                    isPress = false;
                    SendMessage("ktdead");
                }

            }



        }
        if (isChange)
        {
            isChange = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
                {
                    GameObject[] ga = GameObject.FindGameObjectsWithTag(i + "." + j);
                    if (ga.Length >= 2)
                    {
                        //ga[0].gameObject.transform.GetComponent<moveCell>().kt = true;
                        //Destroy(ga[1]);
                        //Score.iScore += (int)Mathf.Pow(2, PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")));
                        //PlayerPrefs.SetInt("Score" + PlayerPrefs.GetInt("Level"), Score.iScore);
                    }
                }
            }
        }
        Spawn();
    }

    void Spawn()
    {
        if (isChuyen)
        {

            if (Time.time - fTime > 0.19f)
            {
                isChuyen = false;
                isChange = true;
                xi = Random.Range(0, 5);
                yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                while (xi % 2 != yj % 2)
                {
                    yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                }
                while (PlayerPrefs.GetInt(xi + "." + yj + PlayerPrefs.GetInt("Level")) > 0)
                {
                    xi = Random.Range(0, 5);
                    while (xi % 2 != yj % 2 || !PlayerPrefs.HasKey(xi + "." + yj + PlayerPrefs.GetInt("Level")))
                    {
                        yj = Random.Range(Mathf.Abs(2 - xi), 9 - Mathf.Abs(2 - xi));
                    }
                }
                arrPos[xi, yj] = new Vector3(0 - fWidCell / 4.6f * (4 - yj), fSince - fHeiCell / 3.1f * (xi - 2), 0);
                int e = Random.Range(0, 9);
                if (e < 6)
                {
                    e = 1;
                }
                else
                    e = 2;
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level"), e);
                Transform trans1 = Instantiate(cell, arrPos[xi, yj], Quaternion.identity) as Transform;
                trans1.tag = xi + "." + yj;
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level") + ".i", xi);
                PlayerPrefs.SetInt(xi + "." + yj + PlayerPrefs.GetInt("Level") + ".j", yj);
            }
        }
    }
}
