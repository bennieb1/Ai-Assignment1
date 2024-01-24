using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fsm : MonoBehaviour
{
  public RuntimeAnimatorController FSMcontroller;

    public Animator FSManimator { get; private set; }


    private void Awake()
    {

        GameObject FSMGo = new GameObject("FSM" , typeof(Animator));
        FSMGo.transform.SetParent(transform);
        FSManimator = GetComponent<Animator>();
        FSManimator.runtimeAnimatorController = FSMcontroller;
        FSMGo.hideFlags = HideFlags.HideInInspector;

        FSMBaseState[] states = FSManimator.GetBehaviours<FSMBaseState>();
        foreach (FSMBaseState state in states)
        {
            state.Init(this, FSMGo);
        }
    }

    public bool ChangeState(string stateName) {
    
        return ChangeState(Animator.StringToHash(stateName));

    }

    public bool ChangeState(int hashStateName)
    {
        bool hasState = true;
#if UNITY_EDITOR
        hasState = FSManimator.HasState(0, hashStateName);
        Debug.Assert(hasState);
#endif
        FSManimator.CrossFade(hashStateName, 0.0f, 0);
        return hasState;
    }
}
