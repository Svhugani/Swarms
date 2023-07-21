using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSettingsAdjuster : MonoBehaviour
{
    [field: SerializeField] public SwarmSettings SwarmSettings { get; private set; }
    [field: SerializeField] public float TimeScaleMinVal { get; private set; } = -1;
    [field: SerializeField] public float TimeScaleMaxVal { get; private set; } = 4;
    [field: SerializeField] public float TimeScaleSensitivity { get; private set; } = 1;

    private void Start()
    {
        AppManager.Instance.InputManager.OnMouseScroll += AdjustTimeScale;
    }

    private void OnDisable()
    {
        AppManager.Instance.InputManager.OnMouseScroll -= AdjustTimeScale;
    }

    private void AdjustTimeScale(float delta)
    {
        SwarmSettings.TimeScaleRuntime += delta * TimeScaleSensitivity;
        SwarmSettings.TimeScaleRuntime = Mathf.Clamp(SwarmSettings.TimeScaleRuntime, TimeScaleMinVal, TimeScaleMaxVal);

    }


}
