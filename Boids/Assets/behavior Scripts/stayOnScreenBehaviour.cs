using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Stay On Screen")]
public class stayOnScreenBehaviour : FlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = center - agent.transform.position;
        //t = 0 when at center, t = 1 when at radius, t > 1 when outside of radius
        float t = centerOffset.magnitude / radius;

        //if within 90% of radius - dont change anything
        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
