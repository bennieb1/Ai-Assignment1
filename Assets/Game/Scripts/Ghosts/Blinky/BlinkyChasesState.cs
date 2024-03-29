using UnityEngine;

public class BlinkyChaseState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public string GoToDieState = "Dead";
    private int gotoDieStateHash;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);
        gotoDieStateHash = Animator.StringToHash(GoToDieState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.killedEvent.AddListener(() => fsm.ChangeState(gotoDieStateHash));
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Target Pac-Man's position
        ghostController.SetMoveToLocation(ghostController.PacMan.transform.position);

        // Check for Pac-Man's invincibility state
        if (GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoRunawayStateHash);
        }
    }
}
