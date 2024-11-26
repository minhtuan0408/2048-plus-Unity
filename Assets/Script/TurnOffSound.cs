using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSound : MonoBehaviour
{
    public GameObject[] GameObject;
    int cnt = 0;
    public void TurnOnOff()
    {
        if (cnt % 2 == 0)
        {
            
            GameObject[0].SetActive(false);
        }
        else
        {
            
            GameObject[0].SetActive(true);
        }

        cnt++;
    }


}
