using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmOrigin : MonoBehaviour
{
    [field: SerializeField] public float SpawnRadius { get; private set; } = 2;
    [field: SerializeField] public Color RangeColor { get; private set; } = Color.white;    
    private void OnDrawGizmos()
    {
        Gizmos.color = RangeColor;
        Gizmos.DrawSphere(transform.position, SpawnRadius);
    }
}
