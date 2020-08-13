using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Comp")]
public class CompBehavior : FlockBehavior
{

    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        //if you dont have a weight for a behavior or vice versa
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        //set up move
        Vector3 move = Vector3.zero;

        //iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
