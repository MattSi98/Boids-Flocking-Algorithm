using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoid Obstacles")]
public class obstacleAvoidanceBehavior : FlockBehavior
{

    public float rayDist = 10.0f;

    RaycastHit hit;

    // Bit shift the index of the layer (8) to get a bit mask
    // use this mask to only collide with bodies inside layer 8 - named obstacles in this case
    //use this mask to only ray cast against obstacles, because the flocking behaviors (allignment/avoidance mainly),
    // already handle in flock collisions
    int layerMask = 1 << 8;


    Vector3 currentVelocity;
    //how long agent should take to turn toward updated move - happens each frame, not based on seconds
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        Vector3 avoidObstMove = Vector3.zero;
        
        
        if (Physics.Raycast(agent.transform.position , agent.transform.forward.normalized, out hit, rayDist, layerMask))
        {
            //forward
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized , Color.blue);

            avoidObstMove = agent.transform.forward.normalized + hit.normal * 20.0f;
            
        }
        else if (Physics.Raycast(agent.transform.position, agent.transform.forward.normalized + agent.transform.right * -1, out hit, rayDist, layerMask))
        {
            //left
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.right * -1, Color.yellow);

            avoidObstMove = agent.transform.forward.normalized + hit.normal * 20.0f;
            
        }
        else if (Physics.Raycast(agent.transform.position, agent.transform.forward.normalized + agent.transform.right, out hit, rayDist, layerMask))
        {
            //right
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.right , Color.red);

            avoidObstMove = agent.transform.forward.normalized + hit.normal * 20.0f;
            
        }
        else if (Physics.Raycast(agent.transform.position, agent.transform.forward.normalized + agent.transform.up, out hit, rayDist, layerMask))
        {
            //up
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.up , Color.green);

            avoidObstMove = agent.transform.forward.normalized + hit.normal * 20.0f;
            
        }
        else if (Physics.Raycast(agent.transform.position, agent.transform.forward.normalized + agent.transform.up * -1, out hit, rayDist, layerMask))
        {
            //down
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.up * -1 , Color.white);

            avoidObstMove = agent.transform.forward.normalized + hit.normal * 20.0f;
            
        }
        else
        {
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized , Color.blue);
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.right , Color.red);
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.right * -1, Color.yellow);
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.up * -1, Color.white);
            //Debug.DrawRay(agent.transform.position, agent.transform.forward.normalized + agent.transform.up, Color.green);
            avoidObstMove = Vector3.zero;
        }

        avoidObstMove = Vector3.SmoothDamp(agent.transform.forward, avoidObstMove, ref currentVelocity, agentSmoothTime);
        return avoidObstMove;
    }

  
}
