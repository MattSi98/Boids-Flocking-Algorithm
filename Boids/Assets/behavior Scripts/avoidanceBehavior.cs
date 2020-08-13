using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class avoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //avoidance rule:
        // if too close to neighbors - move away from their avg position



        //if no neighbors return no adjustment
        //zero vector
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //add all points and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        //if there is no filter - use context
        //if there is a filter use the new filtered context
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position;
            }
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}

