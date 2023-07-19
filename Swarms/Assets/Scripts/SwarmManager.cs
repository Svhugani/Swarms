using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    [field: SerializeField] public SwarmSettings SwarmSettings { get; private set; }
    [field: SerializeField] public AgentActor AgentActorPrefab { get; private set; }
    [field: SerializeField] public Transform AgentActorsContainer { get; private set; }
    [field: SerializeField] public SwarmOrigin SwarmOrigin { get; private set; }
    [field: SerializeField] public SwarmTarget SwarmTarget { get; private set; }
    [field: SerializeField] public LayerMask ObstaclesMask { get; private set; }    
    public List<AgentActor> AgentActors { get; private set; } = new List<AgentActor>();
    public List<Agent> Swarm { get; private set; } = new List<Agent>();



    private void Start()
    {
        GenerateSwarm(SwarmOrigin.SpawnRadius, SwarmOrigin.transform.position);
    }

    private void Update()
    {
        CalculateSwarmPoperties();
        SwarmMovement(Time.deltaTime);
    }




    public void GenerateSwarm(float radius, Vector3 origin)
    {
        for (int i = 0; i < SwarmSettings.SwarmSize; i++) 
        {
            AgentActor agentActor = Instantiate(AgentActorPrefab, AgentActorsContainer);

            Vector3 position = Random.onUnitSphere * radius + origin;
            Vector3 velocity = Random.onUnitSphere;
            float acceleration = Random.Range(SwarmSettings.MinAcceleration, SwarmSettings.MaxAcceleration);
            float speed = Random.Range(SwarmSettings.MinSpeed, SwarmSettings.MaxSpeed);
            Agent agent = new Agent(i, velocity, position, acceleration, agentActor.Collider.radius, speed);

            agentActor.Agent = agent;
            agentActor.transform.position = position;   

            Swarm.Add(agent);
            AgentActors.Add(agentActor);


        }
    }


    public void CalculateSwarmPoperties()
    {
        foreach (AgentActor agentActor in AgentActors)
        {
            AgentMovement(agentActor.Agent);
        }
    }
      
    public void SwarmMovement(float timeDelta)
    {
        foreach (AgentActor agentActor in AgentActors)
        {
            agentActor.UpdateMovement(timeDelta * SwarmSettings.TimeScale);   
        }
    }

    private void AgentMovement(Agent agent)
    {
        Vector3 newVelocity = Vector3.zero;
        Vector3 flockingDirection = Vector3.zero;
        Vector3 alignDirection = Vector3.zero;
        Vector3 collisionDirection = Vector3.zero;
        Vector3 targetDirection = Vector3.zero;
        Vector3 avoidObstacleDirection = Vector3.zero;

        int flockingCounter = 0;
        int alignCounter = 0;
        int collisionCounter = 0;   
        float distance;

        foreach (Agent other in Swarm)
        {
            distance = Vector3.Distance(agent.position, other.position);

            if (agent.id == other.id) continue;
            if (flockingCounter < SwarmSettings.MaxFlockingInputs && distance < SwarmSettings.FlockingRange)
            {
                flockingCounter ++;
                flockingDirection += agent.position;
            }

            if (alignCounter < SwarmSettings.MaxAlignInputs && distance < SwarmSettings.AlignRange))
            {
                alignCounter++;
                alignDirection += other.prevVelocity;
            }

            if (collisionCounter < SwarmSettings.MaxCollisionInputs && distance < SwarmSettings.CollisionRange)
            {
                collisionDirection = -(other.position - agent.position).normalized;
            }
        }


        foreach (Agent other in Swarm)
        {
            if (agent.id == other.id) continue;
            if (IsInCollisionRange(agent, other))
            {
                collisionDirection = - (other.position - agent.position).normalized;
                break;
            }
        }

        if (IsObstacleCollision(agent, out RaycastHit hitInfo))
        {
            avoidObstacleDirection = (hitInfo.collider.transform.position - agent.position).normalized;
        }

        if (SwarmTarget != null && IsInTargetRange(agent))
        {
            targetDirection = (SwarmTarget.transform.position - agent.position).normalized;    
        }

        flockingCounter = Mathf.Max(1, flockingCounter);
        alignCounter = Mathf.Max(1, alignCounter);
        collisionCounter = Mathf.Max(1, collisionCounter);  

        flockingDirection = ((flockingDirection / flockingCounter) - agent.position).normalized;
        alignDirection = (alignDirection / alignCounter).normalized;
        collisionDirection = (collisionDirection / alignCounter).   


        newVelocity = flockingDirection * SwarmSettings.FlockingImpact 
                    + alignDirection * SwarmSettings.AlighImpact 
                    + collisionDirection * SwarmSettings.CollisionImpact
                    + avoidObstacleDirection * SwarmSettings.ObstacleImpact 
                    + targetDirection * SwarmSettings.TargetImpact;


        agent.velocity = newVelocity.normalized;

    }

    private bool IsInFlockingRange(Agent agent, Agent other)
    {
        return AgentComparer(agent, other, SwarmSettings.FlockingRange);
    }

    private bool IsInAlignRange(Agent agent, Agent other)
    {
        return AgentComparer(agent, other, SwarmSettings.AlignRange);
    }

    private bool IsInCollisionRange(Agent agent, Agent other)
    {
        return AgentComparer(agent, other, SwarmSettings.CollisionRange);
    }

    private bool IsInTargetRange(Agent agent)
    {
        return Vector3.Distance(agent.position, SwarmTarget.transform.position) < SwarmSettings.TargetRange;
    }

    private bool AgentComparer( Agent agent, Agent other, float criterion)
    {
        return Vector3.Distance(agent.position, other.position) < criterion;
    }

    private bool IsObstacleCollision(Agent agent, out RaycastHit hitInfo)
    {
        return Physics.SphereCast(agent.position, 
                                  agent.radius, 
                                  agent.prevVelocity, 
                                  out hitInfo, 
                                  SwarmSettings.DetectionRange, 
                                  ObstaclesMask);
    }
}
