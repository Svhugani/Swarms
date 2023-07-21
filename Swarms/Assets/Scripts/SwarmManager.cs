using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : AbstractManager
{
    [field: SerializeField] public SwarmSettings SwarmSettings { get; private set; }
    [field: SerializeField] public AgentActor AgentActorPrefab { get; private set; }
    [field: SerializeField] public Transform AgentActorsContainer { get; private set; }
    [field: SerializeField] public SwarmOrigin SwarmOrigin { get; private set; }
    [field: SerializeField] public SwarmTarget SwarmTarget { get; private set; }
    [field: SerializeField] public LayerMask ObstaclesMask { get; private set; }    
    public List<AgentActor> AgentActors { get; private set; } = new List<AgentActor>();
    public List<Agent> Swarm { get; private set; } = new List<Agent>();
    private float _timer;


    private void Start()
    {
        GenerateSwarm(SwarmOrigin.SpawnRadius, SwarmOrigin.transform.position);
        _timer = 0;
    }

    private void FixedUpdate()
    {
        CalculateSwarmPoperties();
    }

    private void Update()
    {
        SwarmMovement(Time.deltaTime);
        _timer += Time.deltaTime;   
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
            float phase = Random.Range(0, 2 * Mathf.PI);
            Agent agent = new Agent(i, velocity, position, acceleration, agentActor.Collider.radius, speed, phase);

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
            agentActor.UpdateMovement(timeDelta * SwarmSettings.TimeScaleRuntime);   
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
            distance = L2_Distance(agent.position, other.position);

            if (agent.id == other.id) continue;
            if (flockingCounter < SwarmSettings.MaxFlockingInputs && distance < SwarmSettings.FlockingRange)
            {
                flockingCounter ++;
                flockingDirection += agent.position;
            }

            if (alignCounter < SwarmSettings.MaxAlignInputs && distance < SwarmSettings.AlignRange)
            {
                alignCounter++;
                alignDirection += other.prevVelocity;
            }

            if (collisionCounter < SwarmSettings.MaxCollisionInputs && distance < SwarmSettings.CollisionRange)
            {
                collisionCounter ++;
                collisionDirection += (other.position - agent.position).normalized;
            }
        }

        if (IsObstacleCollisionRay(agent, out RaycastHit hitInfo))
        {
            avoidObstacleDirection = (hitInfo.point - agent.position).normalized;
        }

        if(Manager.TargetManager.IsActive())
        {
            targetDirection = (Manager.TargetManager.GetTargetPosition() - agent.position).normalized;
        }
        
        flockingCounter = Mathf.Max(1, flockingCounter);
        flockingDirection = ((flockingDirection / flockingCounter) - agent.position).normalized;
        alignDirection = alignDirection.normalized;

        newVelocity = flockingDirection * SwarmSettings.FlockingImpactRuntime
                    + alignDirection * SwarmSettings.AlignImpactRuntime
                    - collisionDirection * SwarmSettings.CollisionImpactRuntime
                    - avoidObstacleDirection * SwarmSettings.ObstacleImpactRuntime
                    + targetDirection * SwarmSettings.TargetImpactRuntime;

        agent.velocity = newVelocity.normalized;

    }

    private bool IsObstacleCollisionRay(Agent agent, out RaycastHit hitInfo)
    {
        return Physics.Raycast(agent.position,
                          agent.prevVelocity,
                          out hitInfo,
                          SwarmSettings.DetectionRange,
                          ObstaclesMask);
    }

    private float L2_Distance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    private float LINF_Distance(Vector3 a, Vector3 b)
    {
        Vector3 absDiff = new Vector3(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs (a.z - b.z));   
        return Mathf.Max(Mathf.Max(absDiff.x, absDiff.y), absDiff.z);
    }

}
