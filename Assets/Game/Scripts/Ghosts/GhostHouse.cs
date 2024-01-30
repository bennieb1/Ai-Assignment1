using UnityEngine;

public class GhostHouseState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;
    public float releaseDelay; // Time before the ghost is released from the ghost house
    private float releaseTimer; // Timer to track the release time

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        ghostController.SetMoveToLocation(ghostController.ReturnLocation);
        releaseTimer = releaseDelay; // // Set the release timer to the delay
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Decrement the timer by the time elapsed since the last frame
        releaseTimer -= Time.deltaTime;

        // When the timer reaches zero, change the state to 'Chase'
        if (releaseTimer <= 0f)
        {
            fsm.ChangeState(gotoChaseStateHash);
            releaseTimer = releaseDelay; // Reset the timer if needed for future use
        }
    }
}