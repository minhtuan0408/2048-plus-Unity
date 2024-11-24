using UnityEngine;
using System.Collections;

public class ktDeath : MonoBehaviour
{
    static public bool isDie = false;
    private int xRes, yRes;
    private bool kt = false;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void ktdead()
    {
        if (!isDie)
        {
            kt = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
                {
                    if (PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")) > 0)
                    {
                        if (PlayerPrefs.GetInt("R") == 0)
                        {
                            xRes = 0;
                            yRes = 2;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                        if (PlayerPrefs.GetInt("L") == 0)
                        {
                            xRes = 0;
                            yRes = -2;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                        if (PlayerPrefs.GetInt("LT") == 0)
                        {
                            xRes = -1;
                            yRes = -1;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                        if (PlayerPrefs.GetInt("RT") == 0)
                        {
                            xRes = -1;
                            yRes = 1;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                        if (PlayerPrefs.GetInt("RB") == 0)
                        {
                            xRes = 1;
                            yRes = 1;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                        if (PlayerPrefs.GetInt("LB") == 0)
                        {
                            xRes = 1;
                            yRes = -1;
                            if (PlayerPrefs.HasKey((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")))
                                if (PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == 0 || PlayerPrefs.GetInt((i + xRes) + "." + (j + yRes) + PlayerPrefs.GetInt("Level")) == PlayerPrefs.GetInt(i + "." + j + PlayerPrefs.GetInt("Level")))
                                {
                                    kt = true;
                                }
                        }
                    }
                }
            }
        }

        if (!kt)
        {
            isDie = true;
        }

    }
}
