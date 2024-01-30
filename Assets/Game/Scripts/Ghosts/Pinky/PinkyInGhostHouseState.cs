using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyInGhostHouseState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;

    public float initialDelay = 5f; // Time in seconds for Pinky to stay in Ghost House
    private float delayTimer;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        delayTimer = initialDelay; // Reset the timer on state enter
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Count down the timer
        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
        {
            // Time's up, change to chase state
            fsm.ChangeState(gotoChaseStateHash);
        }
    }
}