using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppManager : AbstractManager
{
    public static AppManager Instance { get; private set; }

    [field: SerializeField] public SwarmManager SwarmManager { get; private set; }
    [field: SerializeField] public PostProcessManager PostProcessManager { get; private set; }
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public TargetManager TargetManager { get; private set; }

    private AbstractManager[] _managers;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else Instance = this;
    }

    private void Start()
    {
        _managers = new AbstractManager[] { SwarmManager, PostProcessManager, InputManager, TargetManager };
        _ = Prepare();
    }

    public override async Task Prepare()
    {
        IsBusy = true;
        List<Task> tasks = new List<Task>();

        foreach (var manager in _managers) 
        {
            tasks.Add(manager.Prepare());
        }

        await Task.WhenAll(tasks);
        IsBusy = false;
    }


}
