using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BoardManager : MonoBehaviour
{
    public Transform CellPos;
    public GameObject Cell;
    public float fSince;
    public float fWidCell;
    public float fHeiCell;
    public GameObject CellManager;
    void Start()
    {
        //fWidCell = CellPos.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        //fHeiCell = CellPos.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        //for (int i = 0; i < 5; i++)
        //{
        //    for (int j = 0; j < 5 - Mathf.Abs(2-i); j++)
        //    {
        //        PlayerPrefs.SetInt(i + "." + j, 0);

        //        GameObject newCell = Instantiate(Cell, pos, Quaternion.identity);
        //        newCell.name = "Cell " + i + " " + j;

        //    }
        //}

        for (int i = 0; i < 5; i++)
        {
            for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
            {
                PlayerPrefs.SetInt(i + "." + j, 0);
                Vector3 pos = new Vector3((j - 4) * (fWidCell / 2), (2 - i) * (fHeiCell / 2), 0);
                Instantiate(Cell, pos, Quaternion.identity, CellManager.transform);
            }
        }


    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    int cnt = 0;
        //    Transform parent = CellManager.transform;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        for (int j = Mathf.Abs(2 - i); j < 9 - Mathf.Abs(2 - i); j += 2)
        //        {
        //            Transform child = parent.GetChild(cnt);
        //            child.position = new Vector3((j - 4) * (fWidCell / 2), (2 - i) * (fHeiCell / 2), 0);
        //            cnt++;
        //        }
        //    }
        //}
    }





}
