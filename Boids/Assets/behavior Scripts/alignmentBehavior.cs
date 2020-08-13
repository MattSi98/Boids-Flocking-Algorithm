using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class alignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //alignment rule:
        // find general direction of all neighbors, and try to move in same direction



        //if no neighbors maintain current heading
        //forward vector
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        //add all points and average
        Vector3 alignmentMove = Vector3.zero;

        //if there is no filter - use context
        //if there is a filter use the new filtered context
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context); 
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
