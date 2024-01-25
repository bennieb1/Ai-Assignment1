using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBaseState : FSMBaseState
{

    protected GhostController ghostController;

    public override void Init(GameObject _gameObject, fsm _fsm)
    {
        base.Init( _gameObject , _fsm );
        ghostController = _gameObject.GetComponent<GhostController>();

        // Check if the GhostController component exists on the GameObject
        if (ghostController == null)
        {
            Debug.LogError($"{gameObject.name}GhostBaseState requires a GhostController component on the GameObject.");
        }
    }




}
