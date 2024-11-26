using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        AudioManager.Instance.PlayMusic("Theme");
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
        int occupiedCells = CellList.Count(cell => cell.gameObject?.transform.childCount > 0);

        if (occupiedCells >= 17)
        {
            GameOver.SetActive(true);
            isLose = true;
            Debug.Log("GameOVer");
        }
    }

    public IEnumerator TimeMove()
    {
        CheckChildInCells();
        yield return new WaitForSeconds(0.3f);

        if (!isLose) 
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
        const int maxAttempts = 18; 
        int attempts = 0;
        bool foundEmpty = false;
        Transform emptyCell = null;

        while (attempts < maxAttempts)
        {
            attempts++;
            int cellIndex = UnityEngine.Random.Range(0, amountSpawn + 1);
            emptyCell = parent.GetChild(cellIndex);

            if (emptyCell.childCount == 0) 
            {
                foundEmpty = true;
                break;
            }
        }

        if (foundEmpty)
        {
            GameObject block = Instantiate(obj, emptyCell);
            CellList[emptyCell.GetSiblingIndex()].Block = block.GetComponent<BLock>();
            Debug.Log($"Sinh ở ô {emptyCell.GetSiblingIndex()}");
        }
        else
        {
            Debug.Log("Không còn ô trống! GameOver.");
            GameOver.SetActive(true);
            isLose = true;
        }
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
                    AudioManager.Instance.PlaySFX("Move");
                    StartCoroutine(TimeMove());
                }
                else if (x < 0 && canMove)
                {
                    ticker = 0;
                    slide("A");
                    StartCoroutine(TimeMove());
                    AudioManager.Instance.PlaySFX("Move");
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
                    AudioManager.Instance.PlaySFX("Move");
                }
                if (x > 0 && y < 0 && canMove)
                {
                    ticker = 0;
                    slide("X");
                    StartCoroutine(TimeMove());
                    AudioManager.Instance.PlaySFX("Move");

                }
                if (x < 0 && y > 0 && canMove)
                {
                    ticker = 0;
                    slide("W");
                    StartCoroutine(TimeMove());
                    AudioManager.Instance.PlaySFX("Move");
                }
                if (x < 0 && y < 0 && canMove)
                {
                    ticker = 0;
                    slide("Z");
                    StartCoroutine(TimeMove());
                    AudioManager.Instance.PlaySFX("Move");
                }
               
            }
        }
    }
}
