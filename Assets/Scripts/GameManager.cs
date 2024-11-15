using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] public bool ballOnBoard;
    [SerializeField] public float startTime;
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

    private void Start()
    {

    }

    private void Update()
    {
        startTime -= Time.deltaTime;
        if (startTime <= 0f && ballOnBoard)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        PlayerController.ReleaseBall?.Invoke();
    }
    
}
