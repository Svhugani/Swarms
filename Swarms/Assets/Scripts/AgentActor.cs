using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentActor : MonoBehaviour
{
    [field: SerializeField] public SphereCollider Collider { get; private set; }

    public Agent Agent;

    public void UpdateMovement(float timeDelta)
    {
        Agent.prevVelocity = Agent.velocity;
        transform.forward = Vector3.Slerp(transform.forward, Agent.velocity, timeDelta * Agent.acceleration);    
        transform.position += transform.forward * Agent.speed * timeDelta;
        Agent.position = transform.position;
    }

}
