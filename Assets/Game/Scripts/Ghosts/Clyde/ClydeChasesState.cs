using UnityEngine;

public class ClydeChasesState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public string GoToDieState = "Dead";
    private int gotoDieStateHash;

    private float scatterThreshold = 8.0f; // Distance at which Clyde starts scattering

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

        Vector2 pacmanPosition = ghostController.PacMan.position;
        Vector2 ghostPosition = ghostController.transform.position;
        float distanceToPacman = Vector2.Distance(ghostPosition, pacmanPosition);

        if (distanceToPacman > scatterThreshold)
        {
            ghostController.SetMoveToLocation(pacmanPosition);
        }
        else
        {
            ghostController.SetMoveToLocation(ghostController.ReturnLocation); // Scatter target
        }

        if (GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoRunawayStateHash);
        }
    }
}