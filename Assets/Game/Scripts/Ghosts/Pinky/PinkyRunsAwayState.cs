using UnityEngine;

public class PinkyRunsAwayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            Vector3 playerPosition = ghostController.PacMan.position;
            Vector3 oppositePosition = CalculateOppositePosition(playerPosition);
            ghostController.SetMoveToLocation(oppositePosition);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (GameDirector.Instance.state != GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
    }

    private Vector3 CalculateOppositePosition(Vector3 targetPosition)
    {
        Vector3 ghostPosition = ghostController.transform.position;
        Vector3 directionToTarget = targetPosition - ghostPosition;
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Vector3 oppositePosition = ghostPosition - (directionToTarget * 2) + randomOffset;
        return oppositePosition;
    }
}