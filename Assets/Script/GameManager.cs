using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    // TÌm hiểu LinQ
    public static GameManager Instance;
    public static int ticker;

    private int amountSpawn = 18;
    public GameObject Cell;
    private Transform parent;

    public List<Cell> CellList;

    public static Action<string> slide;


    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 swipeDelta;

    private float minSwipeDistance = 50f;
    private float diagonalSensitivity = 1.5f;

    private bool canMove;
    public int Score;
    private bool isLose = false;
    public GameObject GameOver;



    private void Awake()
    {
        Instance = this;
        parent = transform;
        CellList = new List<Cell>();
        GetAllCell();
        canMove = true;
    }

    private void Start()
    {
        Score = 0;   
        Spawn(Cell);
        Spawn(Cell);

    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Spawn(Cell);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    slide("D");
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    slide("E");
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    slide("W");
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    slide("A");
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    slide("Z");
        //}
        //if(Input.GetKeyDown(KeyCode.X)) { slide("X"); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(Cell);
        }
        DetectSwipe();
    }

    private void CheckChildInCells()
    {
        int cnt = 0;
        foreach (Cell block in CellList)
        {
            if (block.gameObject != null && block.gameObject.transform.childCount > 0) // Kiểm tra child
            {
                cnt++;
                Debug.Log($"Block {block.gameObject.name} có child.");
            }

        }

        if (cnt >= 17)
        {
            GameOver.SetActive(true);
            isLose = true;
        }
    }
    

    public IEnumerator TimeMove()
    {
        CheckChildInCells();
        yield return new WaitForSeconds(0.3f);

        if (!isLose) // Kiểm tra nếu không thua
        {
            canMove = true;
            Spawn(Cell);
        }
        else
        {
            Debug.Log("Thua! Không spawn block.");
        }
    }
    public void Spawn(GameObject obj)
    {
        bool canSpawn = true;
        Transform emptyCell = parent.GetChild(0);
        int cellSpawn = 0;
        int checKEmpty =0;
        while (canSpawn || checKEmpty >18)
        {
            checKEmpty++;
            cellSpawn = UnityEngine.Random.Range(0, amountSpawn + 1);
            emptyCell = parent.GetChild(cellSpawn);
            if (emptyCell.childCount == 0)
            {
                canSpawn = false;
            }

            if (checKEmpty > 19)
            {
                print("GameOver");
                GameOver.SetActive(true );
                isLose = true;
            }

            print(checKEmpty);
        }

        GameObject block = Instantiate(obj, emptyCell);

        CellList[cellSpawn].Block = block.GetComponent<BLock>();
        print("Sinh ở ô " + cellSpawn);
        print(block.name);
    }

    private void GetAllCell() 
    {
        Transform parent = this.transform;

        for (int i = 0; i < parent.childCount; i++)
        {
            Cell addCell = parent.GetChild(i).GetComponent<Cell>();
            CellList.Add(addCell);
        }
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                endTouchPosition = touch.position;
                AnalyzeSwipe();
               
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            AnalyzeSwipe();
            
        }
    }

    private void AnalyzeSwipe()
    {
        swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > minSwipeDistance)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            
            if (Mathf.Abs(x) > Mathf.Abs(y) * diagonalSensitivity)
            {
                // Vuốt ngang
                if (x > 0 && canMove)
                {
                    ticker = 0;
                    slide("D");
                    StartCoroutine(TimeMove());
                }
                else if (x < 0 && canMove)
                {
                    ticker = 0;
                    slide("A");
                    StartCoroutine(TimeMove());
                    
                }
                
            }
            else
            {
                // Vuốt chéo
                if (x > 0 && y > 0 && canMove)
                {
                    ticker = 0;
                    slide("E");
                    StartCoroutine(TimeMove());

                }
                if (x > 0 && y < 0 && canMove)
                {
                    ticker = 0;
                    slide("X");
                    StartCoroutine(TimeMove());

                }
                if (x < 0 && y > 0 && canMove)
                {
                    ticker = 0;
                    slide("W");
                    StartCoroutine(TimeMove());
                }
                if (x < 0 && y < 0 && canMove)
                {
                    ticker = 0;
                    slide("Z");
                    StartCoroutine(TimeMove());
                }
               
            }
        }
    }
}
