using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Human human;
    public float spawnRadius = 10;
    
    
    public void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            var newRadX = Random.Range(-spawnRadius, spawnRadius);
            var newRadY = Random.Range(-spawnRadius, spawnRadius);
            
            var spanwed = Instantiate(human, new Vector3(newRadX, newRadY, 0), Quaternion.identity);
            World.humans.Add(spanwed.transform);
        }       
    }
}