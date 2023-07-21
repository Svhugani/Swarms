using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : AbstractManager
{
    public delegate void MouseScrollHandler(float delta);
    public event MouseScrollHandler OnMouseScroll;

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            OnMouseScroll?.Invoke(Input.mouseScrollDelta.y);
        }

        if (Input.GetKeyDown(KeyCode.E))
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
