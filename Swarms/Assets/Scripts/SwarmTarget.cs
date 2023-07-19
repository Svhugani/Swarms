using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmTarget : MonoBehaviour
{
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField] public float MarkerSize { get; private set; } = .2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawCube(transform.position, MarkerSize * Vector3.one);
    }
}
