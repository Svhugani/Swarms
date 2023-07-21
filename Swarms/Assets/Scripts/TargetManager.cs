using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : AbstractManager
{
    [field: SerializeField] public SwarmTarget SwarmTarget { get; private set; }
    [field: SerializeField] public Transform OrbTransform { get; private set; }
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    
    public void SnapToPlayer()
    {
        SwarmTarget.transform.parent = PlayerTransform; 
        SwarmTarget.transform.localPosition = Vector3.zero; 
    }

    public void SnapToOrb()
    {
        SwarmTarget.transform.parent = OrbTransform;
        SwarmTarget.transform.localPosition = Vector3.zero;
    }


    public void Activate()
    {
        SwarmTarget.IsActive = true;
    }

    public void Deactivate()
    {
        SwarmTarget.IsActive = false;
    }

    public Vector3 GetTargetPosition()
    {
        return SwarmTarget.transform.position;
    }

    public bool IsActive()
    {
        return SwarmTarget.IsActive;
    }
}
