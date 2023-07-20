using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonDOF : MonoBehaviour
{
    [field: SerializeField] public float VisionRadius { get; private set; } = 1.0f;
    [field: SerializeField] public float VisionRange { get; private set; } = 100f;
    [field: SerializeField] public float FocusChangeSpeed { get; private set; } = 2f;
    private float _currentDistance;
    private void Update()
    {
        if (!DetectObjectsRay(out RaycastHit hitInfo)) return;
        _currentDistance = Mathf.Lerp(_currentDistance, hitInfo.distance, Time.deltaTime * FocusChangeSpeed);
        AppManager.Instance.PostProcessManager.SetFocusDistance(_currentDistance);
        Debug.Log("Distance: " +  _currentDistance);    
    }

    private bool DetectObjectsSphere(out RaycastHit hitInfo)
    {
        return Physics.SphereCast(transform.position,
                                  VisionRadius,
                                  transform.forward,
                                  out hitInfo,
                                  VisionRange
                                  );
    }

    private bool DetectObjectsRay(out RaycastHit hitInfo)
    {
        return Physics.Raycast(transform.position,
                                  transform.forward,
                                  out hitInfo,
                                  VisionRange
                                  );
    }
}
