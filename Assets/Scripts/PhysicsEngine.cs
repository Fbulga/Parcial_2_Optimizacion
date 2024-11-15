using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] physicsAffectedGameObjects;

    private Dictionary<int, GameObject> gameObjectsDictionary = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, CustomPhysicsNuestro> physicsDictionary = new Dictionary<GameObject, CustomPhysicsNuestro>();
    

    private void Start()
    {
        for (int i = 0; i < physicsAffectedGameObjects.Length; i++)
        {
            if (DoesPhysicsApply(physicsAffectedGameObjects[i]))
            {
                physicsDictionary.Add(physicsAffectedGameObjects[i],physicsAffectedGameObjects[i].GetComponent<CustomPhysicsNuestro>());
            }

            physicsAffectedGameObjects[i] = null;
        }

    }

    private void Update()
    {
        foreach (var gameObject in physicsDictionary)
        {
            CalculatePhysics(gameObject,Time.deltaTime);
        }
    }

    void CalculatePhysics(KeyValuePair<GameObject,CustomPhysicsNuestro> gameObjectPhysics, float time)
    {
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
