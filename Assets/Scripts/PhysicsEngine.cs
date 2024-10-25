using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] physicsAffectedGameObjects;

    private Dictionary<int, GameObject> gameObjectsDictionary = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, Physics> physicsDictionary = new Dictionary<GameObject, Physics>();
    

    private void Start()
    {
        for (int i = 0; i < physicsAffectedGameObjects.Length; i++)
        {
            if (DoesPhysicsApply(physicsAffectedGameObjects[i]))
            {
                //gameObjectsDictionary.Add(i,physicsAffectedGameObjects[i]);
                physicsDictionary.Add(physicsAffectedGameObjects[i],physicsAffectedGameObjects[i].GetComponent<Physics>());
            }

            physicsAffectedGameObjects[i] = null;
        }

    }

    private void Update()
    {
        /*for (int i = 0; i < gameObjectsDictionary.Count; i++)
        {
            CalculatePhysics(gameObjectsDictionary[i],Time.deltaTime);
        }*/

        foreach (var gameObject in physicsDictionary)
        {
            CalculatePhysics(gameObject,Time.deltaTime);
        }
    }

    void CalculatePhysics(/*GameObject gameObject,*/KeyValuePair<GameObject,Physics> gameObjectPhysics, float time)
    {
        
        /*physicsDictionary[gameObject].velocity += physicsDictionary[gameObject].accelerationApplied * time;
        gameObject.transform.position +=  physicsDictionary[gameObject].velocity * time + physicsDictionary[gameObject].accelerationApplied * (time * time * 0.5f);
        physicsDictionary[gameObject].accelerationApplied = new Vector3(0, 0, 0);*/
     
        gameObjectPhysics.Value.velocity += gameObjectPhysics.Value.accelerationApplied * time;
        gameObjectPhysics.Key.transform.position += gameObjectPhysics.Value.velocity * time + gameObjectPhysics.Value.accelerationApplied * (time * time * 0.5f);
        gameObjectPhysics.Value.accelerationApplied = new Vector3(0, 0, 0);

    }

    bool DoesPhysicsApply(GameObject gameObject)
    {
        if (gameObject.gameObject.TryGetComponent(out IPhysics physics))
        {
            return true;
        }
        return false;
    }
}
