using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Notes
    //scriptable obj - obj that doesnt need to be attached to game obj
public abstract class FlockBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
