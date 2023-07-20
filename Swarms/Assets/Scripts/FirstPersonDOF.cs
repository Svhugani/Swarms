using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonDOF : MonoBehaviour
{
    [field: SerializeField] public float VisionRadius { get; private set; } = 1.0f;
    [field: SerializeField] public float VisionRange { get; private set; } = 100f;
    private void Update()
    {
        if (!DetectObjects(out RaycastHit hitInfo)) return;
        
        AppManager.Instance.PostProcessManager.SetFocusDistance(hitInfo.distance);
    }

    private bool DetectObjects(out RaycastHit hitInfo)
    {
        return Physics.SphereCast(transform.position,
                                  VisionRadius,
                                  transform.forward,
                                  out hitInfo,
                                  VisionRange
                                  );
    }
}
