using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] physicsAffectedGameObjects;

    private void Update()
    {
        foreach (GameObject gameObject in physicsAffectedGameObjects)
        {
            CalculatePhysics(gameObject,Time.deltaTime);
        }
    }

    void CalculatePhysics(GameObject gameObject, float time)
    {
        Physics gameObjectPhysics = gameObject.GetComponent<Physics>();

        gameObjectPhysics.velocity += gameObjectPhysics.accelerationApplied * time;
        gameObject.transform.position += gameObjectPhysics.velocity * time + gameObjectPhysics.accelerationApplied * time * time * 0.5f;

        gameObjectPhysics.accelerationApplied = new Vector3(0, 0, 0);

    }
}
