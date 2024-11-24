using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public GUISkin scoreSkin;
    public float fTimeChangeColor = 0.1f;
    static public int iScore;
    static public int iBest;
    int[] a = new int[3];
    // Update is called once per frame
    void Start()
    {
        if (!PlayerPrefs.HasKey("Win1"))
        {
            PlayerPrefs.SetInt("Win1", 0);
        }
        if (!PlayerPrefs.HasKey("Win2"))
        {
            PlayerPrefs.SetInt("Win2", 0);
        }
        if (!PlayerPrefs.HasKey("Win3"))
        {
            PlayerPrefs.SetInt("Win3", 0);
        }

        iScore = PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("Level"));
        iBest = PlayerPrefs.GetInt("Best" + PlayerPrefs.GetInt("Level"));
        scoreSkin.label.fontSize = Screen.height * 13 / 324;
        a[0] = 0;
        a[1] = 255;
        a[2] = 0;
    }
    void Update()
    {



    }
    void OnDestroy()
    {
        //				for
    }
    void OnGUI()
    {
        //				scoreSkin.label.normal.textColor = new Color (a [1], a [0], a [2]);
        //				if (a [vt] == 0 || a [vt] == 255) {
        //						vt++;
        //						if (vt == 3) {
        //								vt = 0;
        //						}
        //						d *= -1;
        //						a [vt] += d;
        //						
        //				} else {
        //						if (Time.time - fTime > fTimeChangeColor) {
        //								fTime = Time.time;
        //								a [vt] += d;
        //						}
        //				}
        if (ktDeath.isDie)
        {
            scoreSkin.box.fontSize = Screen.height * 30 / 324;
            GUI.Box(new Rect(Screen.width / 2 - Screen.height / 2, Screen.height / 2 - Screen.height / 3.5f, Screen.height, Screen.height / 2), "Your score:\n" + iScore, scoreSkin.box);

        }
        if (PlayerPrefs.GetInt("Win" + PlayerPrefs.GetInt("Level")) == 1)
        {
            scoreSkin.box.fontSize = Screen.height * 30 / 324;
            GUI.Box(new Rect(Screen.width / 2 - Screen.height / 2, Screen.height / 2 - Screen.height / 3.5f, Screen.height, Screen.height / 2), "You Win\n" + iScore, scoreSkin.box);
            scoreSkin.button.fontSize = Screen.height * 18 / 324;
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.height / 7f, Screen.height / 2 + Screen.height / 12f, Screen.height / 3.5f, Screen.height / 8), "Continue", scoreSkin.button))
            {
                PlayerPrefs.SetInt("Win" + PlayerPrefs.GetInt("Level"), 2);
            }
        }
        scoreSkin.label.fontSize = Screen.height * 28 / 324;
        GUI.Label(new Rect(0, 0 + Screen.height / 14.7f, Screen.height / 4.5f, Screen.height / 12), "2048", scoreSkin.label);
        scoreSkin.box.fontSize = Screen.height * 9 / 324;
        GUI.Box(new Rect(Screen.width - Screen.height / 3.11f, 0 + Screen.height / 10.2f, Screen.height / 6, Screen.height / 16), "" + iScore, scoreSkin.box);
        GUI.Box(new Rect(Screen.width - Screen.height / 6.1f, 0 + Screen.height / 10.2f, Screen.height / 6, Screen.height / 16), "" + iBest, scoreSkin.box);
        scoreSkin.button.fontSize = Screen.height * 18 / 324;
        if (ktDeath.isDie && PlayerPrefs.GetInt("Win" + PlayerPrefs.GetInt("Level")) != 1 && GUI.Button(new Rect(Screen.width / 2 - Screen.height / 8f, Screen.height / 2 + Screen.height / 12f, Screen.height / 4, Screen.height / 8), "Replay", scoreSkin.button))
        {
            if (PlayerPrefs.GetInt("Best" + PlayerPrefs.GetInt("Level")) < iScore)
            {
                PlayerPrefs.SetInt("Best" + PlayerPrefs.GetInt("Level"), iScore);
            }
            PlayerPrefs.SetInt("Score" + PlayerPrefs.GetInt("Level"), 0);
            for (int i = 0; i < 5; i++)
            {
                for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
                {
                    PlayerPrefs.SetInt(i + "." + j + PlayerPrefs.GetInt("Level"), 0);
                }
            }
            PlayerPrefs.SetInt("Win" + PlayerPrefs.GetInt("Level"), 0);
            Application.LoadLevel(Application.loadedLevel);
        }



    }

}
