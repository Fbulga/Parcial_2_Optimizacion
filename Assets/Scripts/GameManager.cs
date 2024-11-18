using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action OnBrickDestroyed;
    [SerializeField] private int points;
    private bool isTimerGoing = true;
    
    public bool ballOnBoard;
    public float startTime;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        OnBrickDestroyed += PointsUp;
    }

    private void OnDisable()
    {
        OnBrickDestroyed -= PointsUp;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (!isTimerGoing) return;
        
        startTime -= Time.deltaTime;
        if (startTime <= 0f && ballOnBoard)
        {
            isTimerGoing = false;
            StartGame();
        }
    }

    private void StartGame()
    {
        PlayerController.ReleaseBall?.Invoke();
    }

    private void PointsUp()
    {
        points++;
    }
    
}
