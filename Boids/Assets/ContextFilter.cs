using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
    public abstract List<Transform> filter(FlockAgent agent, List<Transform> original);
}
