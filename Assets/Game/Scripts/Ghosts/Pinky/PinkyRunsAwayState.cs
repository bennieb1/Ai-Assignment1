using UnityEngine;

public class PinkyRunsAwayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;
    public float evasionDistance = 10f; // Adjust based on level size and gameplay speed

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        EvadePacMan();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Continuously update evasion to make it more dynamic
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
            Vector3 playerDirection = ghostController.PacMan.forward; // Assuming PacMan has a forward direction indicating his current movement direction
            Vector3 futurePosition = playerPosition + playerDirection * evasionDistance; // Predict where Pac-Man is heading
            Vector3 oppositeDirection = CalculateEvasionDirection(playerPosition, futurePosition);
            ghostController.SetMoveToLocation(oppositeDirection);
        }
    }

    private Vector3 CalculateEvasionDirection(Vector3 currentPos, Vector3 futurePos)
    {
        Vector3 ghostPosition = ghostController.transform.position;
        Vector3 directionToFuturePos = futurePos - ghostPosition;

        // Calculate a direction that is perpendicular to the direction towards Pac-Man's future position
        Vector3 perpendicularDirection = Vector3.Cross(directionToFuturePos, Vector3.up).normalized;

        // Use a random factor to decide whether to go left or right
        float randomFactor = Random.Range(0, 2) * 2 - 1; // Will be either -1 or 1
        Vector3 evasionDirection = ghostPosition + perpendicularDirection * randomFactor * evasionDistance;

        // Add randomness to make Pinky's movement less predictable
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        evasionDirection += randomOffset;

        return evasionDirection;
    }
}