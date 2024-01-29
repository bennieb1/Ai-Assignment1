using UnityEngine;

public class ClydeInGhostHouseState : GhostBaseState
{
    public string GotoChaseStateHash = "Chase";
    private int gotoChaseStateHash;
    private int totalDots = 240; // Hardcoded total dots for a standard level
    private int dotsEatenThreshold;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GotoChaseStateHash);
        dotsEatenThreshold = totalDots / 3; // A third of the total dots
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Logic for Clyde when in the ghost house
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameDirector.Instance.ScoreValue >= dotsEatenThreshold)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
    }
}