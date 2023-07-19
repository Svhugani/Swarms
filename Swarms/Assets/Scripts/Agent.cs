using UnityEngine;

[System.Serializable]   
public class Agent 
{
    public readonly int id;
    public Vector3 velocity;
    public Vector3 prevVelocity;
    public Vector3 position;
    public float acceleration;
    public float speed;
    public float radius;

    public Agent(int id, Vector3 velocity, Vector3 position, float acceleration, float radius, float speed)
    {
        this.id = id;
        this.velocity = velocity; 
        this.position = position;
        this.prevVelocity = velocity;    
        this.acceleration = acceleration;
        this.radius = radius;   
        this.speed = speed;
    }

}
