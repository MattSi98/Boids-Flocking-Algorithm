using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    //range slider for default values of stuff
    [Range(0, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f; //how many cna spawn per unit area

    [Range(1f, 100f)]
    public float driveFactor = 10f; //essentially their speed

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidRadiusMultip = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidRadius;
    public float SquareAvoidRadius => squareAvoidRadius;


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidRadius = squareNeighborRadius * avoidRadiusMultip * avoidRadiusMultip;

        //init the flock in random rotations and positions
        //add then to the flcok array
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.one * Random.Range(0,360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            //list of agents in close proximity to agent
            List<Transform> context = GetNearbyObjects(agent);

            //generates move for each agent, based on behaviors
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            //ensures speed not greater than max speed
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            //moves agent
            agent.Move(move);
        }
    }

    //checks colliders - if they are colliding, then add that pos to the lsit
    //aka that agent is a neighbor
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach(Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
