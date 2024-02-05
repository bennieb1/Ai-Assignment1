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
        // Initial evasion
        EvadePacMan();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Continuously evade Pac-Man
        EvadePacMan();

        // Return to chase state once Pac-Man is no longer invincible
        if (GameDirector.Instance.state != GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
    }

    private void EvadePacMan()
    {
        if (ghostController != null)
        {
            Vector3 playerPosition = ghostController.PacMan.position;
            Vector3 oppositePosition = CalculateDynamicEscapeRoute(playerPosition);
            ghostController.SetMoveToLocation(oppositePosition);
        }
    }

    private Vector3 CalculateDynamicEscapeRoute(Vector3 targetPosition)
    {
        Vector3 ghostPosition = ghostController.transform.position;
        Vector3 directionToTarget = targetPosition - ghostPosition;

        // Enhance the escape logic with a more dynamic approach
        Vector3 escapeVector = Vector3.zero;
        // Example: use pathfinding to find the best escape route or simply move in the opposite direction
        escapeVector = ghostPosition - directionToTarget.normalized * Random.Range(5f, 10f); // Adjust the multiplier for escape distance

        // Adding some randomness to the escape route
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        escapeVector += randomOffset;

        return escapeVector;
    }
}