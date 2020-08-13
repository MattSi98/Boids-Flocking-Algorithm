using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//require component on this
[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock => agentFlock;

    //getter for the agents collider
    Collider agentCollider;
    public Collider AgentCollider => agentCollider;
    
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    //sets an agent to be part of a certain flock - cant now differentiate between different groups
    public void initialize(Flock flock)
    {
        agentFlock = flock;
    }

    //turn agent toward vector and move in that direction
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
