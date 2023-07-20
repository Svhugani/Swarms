using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : AbstractManager
{
    [field: SerializeField] public Volume Volume { get; private set; }
    private VolumeProfile _profile;
    private DepthOfField _depthOfField;

    private void Awake()
    {
        _profile = Volume.profile;
        _profile.TryGet(out _depthOfField);

    }

    public void SetFocusDistance(float distance)
    {
        _depthOfField.focusDistance.Override(distance);
    }

}
