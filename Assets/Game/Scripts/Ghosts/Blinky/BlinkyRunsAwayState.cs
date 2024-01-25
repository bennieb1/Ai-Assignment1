using UnityEngine;

public class BlinkyRunsAwayState : GhostBaseState
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

            // Set the ghost's destination to run away from Pac-Man.
            ghostController.SetMoveToLocation(oppositePosition);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoChaseStateHash));
        }
    }
    private Vector3 CalculateOppositePosition(Vector3 targetPosition)
    {

        Vector3 ghostPosition = ghostController.transform.position;
        Vector3 directionToTarget = targetPosition - ghostPosition;
        Vector3 oppositePosition = ghostPosition - (directionToTarget * 2);

        return oppositePosition;
    }
}