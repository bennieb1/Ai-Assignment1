using UnityEngine;

public class InkyInGhostHouseState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;
    private int dotsEatenThreshold = 1500; // Inky leaves after 30 dots are eaten

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Logic for Inky when in the ghost house
        // For example, set Inky's position and animation
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if enough dots have been eaten for Inky to leave the ghost house
        if (GameDirector.Instance.ScoreValue >= dotsEatenThreshold)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
        // Optionally, implement Inky's behavior while waiting in the ghost house
    }
}