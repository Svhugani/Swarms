using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwarmSettings", menuName = "Scriptables/Swarm Settings")]
public class SwarmSettings : ScriptableObject
{
    [field: SerializeField, Header("Main parameters")] public int SwarmSize { get; private set; } = 100;
    [field: SerializeField] public float TimeScale { get; private set; } = 1;
    [field: SerializeField] public float MinAcceleration { get; private set; } = 1f;
    [field: SerializeField] public float MaxAcceleration { get; private set; } = 2f;
    [field: SerializeField] public float MinSpeed { get; private set; } = 1;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 1.5f;
    [field: SerializeField, Range(-10f, 10f)] public float FlockingImpact { get; private set; } = 1.7f;
    [field: SerializeField, Range(-10f, 10f)] public float AlighImpact { get; private set; } = 1.3f;
    [field: SerializeField, Range(-10f, 10f)] public float CollisionImpact { get; private set; } = 5.4f;
    [field: SerializeField, Range(-10f, 10f)] public float TargetImpact { get; private set; } = 2.7f;
    [field: SerializeField, Range(-10f, 10f)] public float ObstacleImpact { get; private set; } = 10f;
    [field: SerializeField] public float FlockingRange { get; private set; } = 5f;
    [field: SerializeField] public float AlignRange { get; private set; } = 5f;
    [field: SerializeField] public float CollisionRange { get; private set; } = 5f;
    [field: SerializeField] public float DetectionRange { get; private set; } = 5f;
    [field: SerializeField] public int MaxFlockingInputs { get; private set; } = 5;
    [field: SerializeField] public int MaxAlignInputs { get; private set; } = 5;
    [field: SerializeField] public int MaxCollisionInputs { get; private set; } = 5;

    public float FlockingImpactRuntime { get; set; }
    public float TargetImpactRuntime { get; set;}
    public float ObstacleImpactRuntime { get; set;}
    public float CollisionImpactRuntime { get; set;}    
    public float AlignImpactRuntime { get; set;}
    public float TimeScaleRuntime { get; set;}  

    private void Awake()
    {
        FlockingImpactRuntime = FlockingImpact;
        TargetImpactRuntime = TargetImpact; 
        ObstacleImpactRuntime = ObstacleImpact;
        CollisionImpactRuntime = CollisionImpact;
        AlignImpactRuntime = AlignImpactRuntime;
        TimeScaleRuntime = TimeScale;
    }


}
