using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohession")]
public class steeredCohessionBehavior : FilteredFlockBehavior
{

    Vector3 currentVelocity;
    //how long agent should take to turn toward updated move - happens each frame, not based on seconds
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //cohession rule:
        // find middle point of all neighbors, and try to move there

        //if no neighbors return no adjustment
        //zero vector
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //add all points and average
        Vector3 cohessionMove = Vector3.zero;

        //if there is no filter - use context
        //if there is a filter use the new filtered context
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohessionMove += item.position;
        }
        cohessionMove /= context.Count;

        //create offset from agent position instead of global offset
        cohessionMove -= agent.transform.position;

        //smooths the transition from current vector to target vector over time
        cohessionMove = Vector3.SmoothDamp(agent.transform.forward, cohessionMove, ref currentVelocity, agentSmoothTime);

        return cohessionMove;
    }
}
