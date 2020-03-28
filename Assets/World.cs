using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class World : MonoBehaviour
    {
        public float spawnRadius = 10;
        public static World Instance;
        public static List<Human> humans = new List<Human>();

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InfectRandomPerson();
            }
        }

        public Vector3 GetRandomPoint()
        {
            var newRadX = Random.Range(-Instance.spawnRadius, Instance.spawnRadius);
            var newRadY = Random.Range(-Instance.spawnRadius, Instance.spawnRadius);
            
            return new Vector3(newRadX, newRadY, 0);
        }

        public void InfectRandomPerson()
        {
            var rand = Random.Range(0, humans.Count - 1);
            var patientZero = humans[rand];
            patientZero.GetComponent<Human>().AttemptToInfect();
        }
    }
}