using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLock : MonoBehaviour
{
    public Sprite[] Image;
    private SpriteRenderer spriteRender;
    public int speed;
    private bool hasCombine;
    public int currentLevel;
    private void Awake()
    {
        currentLevel = 1;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRender.sprite = Image[currentLevel];
    }

    private void Update()
    {
        if (transform.localPosition != Vector3.zero) 
        {
            hasCombine = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, Time.deltaTime * speed);
        }
        else if (hasCombine == false)
        {
            if (transform.parent.GetChild(0) != this.transform)
            {
                
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombine = true;
        }

        
    }

    public void Double()
    {
        currentLevel++;
        spriteRender.sprite = Image[currentLevel];
        GameManager.Instance.Score += (int)Math.Pow(2, (double)currentLevel);


    }

}
