using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirstPersonDOF : MonoBehaviour
{
    [field: SerializeField] public float VisionRadius { get; private set; } = 1.0f;
    [field: SerializeField] public float VisionRange { get; private set; } = 100f;
    [field: SerializeField] public float FocusChangeSpeed { get; private set; } = 2f;
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    private float _currentDistance;
    private float _targetDistance;
    private Vector3 _currentHitPosition;
    private void Update()
    {
        if (DetectObjectsSphere(out RaycastHit hitInfo))
        {
            _targetDistance = Vector3.Distance(transform.position, hitInfo.point);
            _currentHitPosition = hitInfo.point;
        }
        _currentDistance = Mathf.Lerp(_currentDistance, _targetDistance, Time.deltaTime * FocusChangeSpeed);
        AppManager.Instance.PostProcessManager.SetFocusDistance(_currentDistance);
    }

    private bool DetectObjectsSphere(out RaycastHit hitInfo)
    {
        return Physics.SphereCast(transform.position,
                                  VisionRadius,
                                  transform.forward,
                                  out hitInfo,
                                  VisionRange,
                                  LayerMask
                                  );
    }

    private bool DetectObjectsRay(out RaycastHit hitInfo)
    {
        return Physics.Raycast(transform.position,
                                  transform.forward,
                                  out hitInfo,
                                  VisionRange,
                                  LayerMask
                                  );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_currentHitPosition, VisionRadius);
    }
}
