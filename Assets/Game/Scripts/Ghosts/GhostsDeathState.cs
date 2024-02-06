using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class GhostsDeathState : GhostBaseState
{
    public string GoToReturnState = "Return";
    private int gotoReturnStateHash;

    public float respawnDelay = 3f;
    public float respawnTimer = 0f;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoReturnStateHash = Animator.StringToHash(GoToReturnState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
 
        respawnTimer = respawnDelay;
        Debug.Log("Damn I'm dead..... ");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0f)
        {
            fsm.ChangeState(gotoReturnStateHash);
        }
    }
  
}
