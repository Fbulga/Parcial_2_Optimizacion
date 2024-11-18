using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PhysicsEngine : CustomBehaviour
{
    [SerializeField] private GameObject[] physicsAffectedGameObjects;

    private Dictionary<int, GameObject> gameObjectsDictionary = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, CustomPhysicsNuestro> physicsDictionary = new Dictionary<GameObject, CustomPhysicsNuestro>();
    

    protected override void CustomStart()
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

    protected override void CustomFixedUpdate()
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
