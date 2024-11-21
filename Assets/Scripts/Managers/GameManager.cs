using System;
using DefaultNamespace;
using UnityEngine;


public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action OnBrickDestroyed;
    public Action OnBrickCreated;
    [SerializeField] private int points;
    [SerializeField] private int BricksRemaining;
    [SerializeField] private int Health;
    [SerializeField] private int activeBalls;
    [SerializeField] private PlayerController player;
    public bool ballOnBoard;
    
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
        OnBrickCreated += AddBrick;
    }

    private void OnDisable()
    {
        OnBrickDestroyed -= PointsUp;
        OnBrickCreated -= AddBrick;
    }
    

    private void PointsUp()
    {
        points++;
        BricksRemaining--;
        if (BricksRemaining <= 0)
        {
            Debug.Log("Game Over, ganaste");
        }
    }

    public void AddBrick()
    {
        BricksRemaining++;
    }
    public void HealthUp()
    {
        Health++;
    }
    public void HealthDown()
    {
        Health--;
        player.Parent();
        ballOnBoard = true;
        GameOver();
    }
    
    public void SetPLayerInstance(PlayerController _player)
    {
        player = _player;
    }
    public void ActiveBallsUp()
    {
        activeBalls++;
    }
    public void ActiveBallsDown()
    {
        activeBalls--;
        if (activeBalls <= 0)
        {
            HealthDown();
        }
    }

    private void GameOver()
    {
        if (Health <= 0) Debug.Log("Game Over, perdiste");
    }
    
}
