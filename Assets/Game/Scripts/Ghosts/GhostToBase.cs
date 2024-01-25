using UnityEngine;

public class GhostToBase : GhostBaseState
{
    public string GoToRespawnState = "Respawn";
    private int gotoRespawnState;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRespawnState = Animator.StringToHash(GoToRespawnState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.SetMoveToLocation(ghostController.ReturnLocation);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoRespawnState));
        }
    }
}
