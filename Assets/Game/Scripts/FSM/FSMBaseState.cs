using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBaseState : StateMachineBehaviour
{
  
    protected fsm fsm { get; private set; }
    protected GameObject gameObject { get; private set; }

    public virtual void Init(fsm _fsm, GameObject _gameObject)
    {
        fsm = _fsm;
        gameObject = _gameObject;
    }

  
}
