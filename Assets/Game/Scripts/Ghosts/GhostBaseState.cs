using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBaseState : FSMBaseState
{

    protected GhostController ghostController;

    public override void Init(fsm _fsm, GameObject _gameObject)
    {
        base.Init(_fsm, _gameObject);
        ghostController = _gameObject.GetComponent<GhostController>();

        // Check if the GhostController component exists on the GameObject
        if (ghostController == null)
        {
            Debug.LogError("GhostBaseState requires a GhostController component on the GameObject.");
        }
    }




}
