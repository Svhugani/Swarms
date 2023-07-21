using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : AbstractManager
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Manager.TargetManager.Activate();
            Manager.TargetManager.SnapToPlayer();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Manager.TargetManager.Activate();
            Manager.TargetManager.SnapToOrb();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            Manager.TargetManager.Deactivate();
        }
    }
}
