using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Enums;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
        public class PowerUpPool : MonoBehaviour
    {
        [SerializeField] private GameObject[] powerUpPrefabs; 
        [SerializeField] private PowerUpType[] powerUpTypes; 
        [SerializeField] private PhysicsEngine physicsEngine;
        [SerializeField, Range(0f, 1f)] private float powerUpChance;
        private Dictionary<PowerUpType, GameObject> powerUpDictionary; 
        private Dictionary<PowerUpType, Queue<GameObject>> pool; 
        public static PowerUpPool Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            powerUpDictionary = new Dictionary<PowerUpType, GameObject>();
            pool = new Dictionary<PowerUpType, Queue<GameObject>>();

            for (int i = 0; i < powerUpPrefabs.Length; i++)
            {
                powerUpDictionary.Add(powerUpTypes[i], powerUpPrefabs[i]);
                pool.Add(powerUpTypes[i], new Queue<GameObject>());
            }
        }

        public GameObject GetPowerUp(PowerUpType type)
        {
            if (pool[type].Count > 0)
            {
                var powerUp = pool[type].Dequeue();
                powerUp.SetActive(true);
                return powerUp;
            }
            
            var newPowerUp = Instantiate(powerUpDictionary[type]);
            physicsEngine.AddObjet(newPowerUp);
            return newPowerUp;
        }

        public void ReturnPowerUp(GameObject powerUp, PowerUpType type)
        {
            powerUp.SetActive(false);
            pool[type].Enqueue(powerUp);
        }

        public void TryDropPowerUp(Vector3 position)
        {
            if (Random.value > powerUpChance)
            {
                var randomType = powerUpTypes[Random.Range(0, powerUpTypes.Length)];
                var powerUp = GetPowerUp(randomType);
                
                powerUp.transform.position = position;
                powerUp.SetActive(true);
            }
        }
    }
}