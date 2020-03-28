using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Human : MonoBehaviour
{
    private float _maxCountDown = 5;
    private float _countDown;
    private Vector3 _targetSpot;

    private HumanHealthStatus _healthStatus;
    private SpriteRenderer _body;

    

    private float infectionCountDown = 0.5f;

    private float cureTime = 6f;

    private void Awake()
    {
        _countDown = _maxCountDown;
        _healthStatus = new HumanHealthStatus();
        _body = GetComponent<SpriteRenderer>();
        _body.color = Color.green;
    }


    private void Cure()
    {
        _healthStatus.CurrentStatus = HumanHealthStatus.Status.Removed;
        _body.color = Color.yellow;
    }
    
    private void Update()
    {
        infectionCountDown -= Time.deltaTime;
        

        if (_healthStatus.CurrentStatus == HumanHealthStatus.Status.Infected)
        {
            cureTime -= Time.deltaTime;
            if (cureTime <= 0)
            {
                Cure();
            }
        }

        
        WanderToRandomPoint();
        CheckNeighborsInRadius();
    }

    private void CheckNeighborsInRadius()
    {
        foreach (Transform tr in World.humans)
        {
            float distanceSqr = (transform.position - tr.position).sqrMagnitude;

            if (distanceSqr < 0.4 && tr.gameObject != gameObject)
            {
                var human = tr.GetComponent<Human>();

                if (_healthStatus.CurrentStatus == HumanHealthStatus.Status.Infected)
                {
                    if (infectionCountDown <= 0)
                    {
                        infectionCountDown = 0.5f;
                        human.AttemptToInfect();    
                    }
                    
                }
            }
        }
    }

    private void WanderToRandomPoint()
    {
        _countDown -= Time.deltaTime;

        if (_countDown <= 0 || Vector3.Distance(transform.position, _targetSpot) < 0.5f)
        {
            _targetSpot = World.Instance.GetRandomPoint();
            _countDown = 5;
        }

        float step = 1 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _targetSpot, step);
    }

    public bool AttemptToInfect()
    {
        if (_healthStatus.CurrentStatus == HumanHealthStatus.Status.Infected)
            return false;

        if (_healthStatus.CurrentStatus == HumanHealthStatus.Status.Removed)
            return false;

        var chance = Random.Range(0, 10);
            
        if (chance <= 1)
        {
            _body.color = Color.red;
            _healthStatus.CurrentStatus = HumanHealthStatus.Status.Infected;
            return true;
        }

        return false;
    }
}