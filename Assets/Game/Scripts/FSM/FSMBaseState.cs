using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBaseState : StateMachineBehaviour
{
  
    protected fsm fsm { get; private set; }
    protected GameObject gameObject { get; private set; }

    public virtual void Init(GameObject _gameObject, fsm _fsm )
    {
        fsm = _fsm;
        gameObject = _gameObject;
    }

  
}
