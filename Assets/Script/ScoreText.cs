using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI _scoreDisplay;


    private void Awake()
    {
        _scoreDisplay = GetComponent<TextMeshProUGUI>();
    }
    void LateUpdate()
    {
        _scoreDisplay.text = ("Score : ") + GameManager.Instance.Score.ToString();
    }
}
