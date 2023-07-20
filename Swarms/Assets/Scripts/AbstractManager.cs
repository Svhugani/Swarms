using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AbstractManager : MonoBehaviour
{
    public bool IsBusy { get; protected set; }
    protected AppManager Manager { get { return AppManager.Instance; } }    

    public virtual async Task Prepare()
    {
        await Task.Yield();
    }
}
