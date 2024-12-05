using System;
using Controllers;
using Scriptables;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Action OnBrickDestroyed;
        public Action OnBrickCreated;
        public Action OnBallsUp;
        public Action OnBallsDown;
        public Action OnHealthUp;
        public Action OnRestart;
        private float ballBaseSpeed;
        private HealthAndPoints ui;
        
        public int points;
        [SerializeField] private int BricksRemaining;
        [SerializeField] private int Health;
        [SerializeField] private int activeBalls;
        [SerializeField] private PlayerController player;
        [SerializeField] private DeadZone deadZone;
        [SerializeField] private BallData ballData;
        private bool isSecondLevel = false;

        
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
                ballBaseSpeed = ballData.MaxSpeed;
            }
        }

        private void OnEnable()
        {
            OnBrickDestroyed += PointsUp;
            OnBrickCreated += AddBrick;
            OnRestart += Restart;
            OnBallsUp += ActiveBallsUp;
            OnBallsDown += ActiveBallsDown;
            OnHealthUp += HealthUp;
        }

        private void OnDisable()
        {
            OnBrickDestroyed -= PointsUp;
            OnBrickCreated -= AddBrick;
            OnRestart -= Restart;
            OnBallsUp -= ActiveBallsUp;
            OnBallsDown -= ActiveBallsDown;
            OnHealthUp -= HealthUp;
        }
        

        private void PointsUp()
        {
            points++;
            UpdatePoints();
            BricksRemaining--;
            if (BricksRemaining <= 0)
            {
                if (isSecondLevel) SceneManager.LoadScene(2);
                else
                {
                    SceneManager.LoadScene(4);
                    Restart();
                    isSecondLevel = true;
                }
            }
        }

        void UpdatePoints()
        {
            ui.UpdatePoints(points);
        }
        
        private void AddBrick()
        {
            BricksRemaining++;
        }
        public void StartDeadlyTimer()
        {
            deadZone.RunDeadlyTimer();
        }
        public void SetPointsAndHealthUI(HealthAndPoints points)
        {
            ui = points;
        }
        public void ChangeBallsMaxSpeed()
        {
            ballData.SetMaxSpeed(ballBaseSpeed * 0.7f);
            player.ChangeBallSpeed();
        }
        public void ResetBallsMaxSpeed()
        {
            ballData.SetMaxSpeed(ballBaseSpeed);
        }

        private void Restart()
        {
            ballOnBoard = true;
            points = 0;
            BricksRemaining = 0;
            Health = 300;
            activeBalls = 1;
            ballData.SetMaxSpeed(9);
            isSecondLevel = false;
        }

        private void HealthUp()
        {
            Health++;
        }

        private void HealthDown()
        {
            ballOnBoard = true;
            Health--;
            ui.UpdateHealth(Health);
            ActiveBallsUp();
            player.Parent();
            GameOver();
        }

        public void SetPLayerInstance(PlayerController _player)
        {
            player = _player;
        }
        public void SetLongerPLayer()
        {
            player.LongerBar(ballOnBoard);
        }
        public void SetDeadZone(DeadZone _zone)
        {
            deadZone = _zone;
        }

        private void ActiveBallsUp()
        {
            activeBalls++;
        }

        private void ActiveBallsDown()
        {
            activeBalls--;
            if (activeBalls <= 0)
            {
                HealthDown();
            }
        }

        private void GameOver()
        {
            if (Health <= 0) SceneManager.LoadScene(3);;
        }
    }
}