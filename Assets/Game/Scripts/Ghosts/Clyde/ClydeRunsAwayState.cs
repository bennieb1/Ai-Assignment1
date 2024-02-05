using UnityEngine;

public class ClydeRunsAwayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;
    // Define waypoints or corners where Clyde might retreat to
    private Vector3[] safeCorners;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);

        // Initialize safe corners with positions. These should be set to specific locations on your map.
        safeCorners = new Vector3[] {
            new Vector3(1f, -2f, 0f), // Example corner positions
            new Vector3(17f, -2f, 0f),
            new Vector3(1f, -21f, 0f),
            new Vector3(17f, -21f, 0f)
        };
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        ChooseAndSetRetreatDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Optionally, Clyde can re-evaluate his destination periodically to adapt to changing situations
        ChooseAndSetRetreatDestination();

        // Return to chase state once Pac-Man is no longer invincible
        if (GameDirector.Instance.state != GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
    }

    private void ChooseAndSetRetreatDestination()
    {
        if (ghostController != null)
        {
            Vector3 playerPosition = ghostController.PacMan.position;
            Vector3 retreatDestination = ChooseFurthestSafeCorner(playerPosition);
            ghostController.SetMoveToLocation(retreatDestination);
        }
    }

    private Vector3 ChooseFurthestSafeCorner(Vector3 playerPosition)
    {
        // Find the corner that is furthest from Pac-Man to maximize the chance of evasion
        Vector3 furthestCorner = safeCorners[0];
        float maxDistance = 0f;

        foreach (var corner in safeCorners)
        {
            float distance = (playerPosition - corner).sqrMagnitude; // Use squared magnitude for efficiency
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestCorner = corner;
            }
        }

        return furthestCorner;
    }
}