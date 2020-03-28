using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealthStatus
{
    public enum Status
    {
        Healthy,
        Infected,
        Removed
    }

    public Status CurrentStatus = Status.Healthy;
}
