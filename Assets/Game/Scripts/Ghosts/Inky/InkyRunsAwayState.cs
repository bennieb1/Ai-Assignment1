using UnityEngine;

public class InkyRunsAwayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;
    // Reference to Blinky's position, assuming there's a way to access it
    private Transform blinkyTransform;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
        // Initialize Blinky's transform. This should be set to the actual transform of Blinky in your game.
        blinkyTransform = GameObject.Find("Blinky").transform; // Example tag, adjust based on your game setup
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CalculateAndSetEscapeRoute();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Continuously calculate escape route to adapt to changes in Pac-Man and Blinky's positions
        CalculateAndSetEscapeRoute();

        // Return to chase state once Pac-Man is no longer invincible
        if (GameDirector.Instance.state != GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }
    }

    private void CalculateAndSetEscapeRoute()
    {
        if (ghostController != null && blinkyTransform != null)
        {
            Vector3 playerPosition = ghostController.PacMan.position;
            Vector3 blinkyPosition = blinkyTransform.position;
            Vector3 escapeRoute = CalculateEscapeRoute(playerPosition, blinkyPosition);
            ghostController.SetMoveToLocation(escapeRoute);
        }
    }

    private Vector3 CalculateEscapeRoute(Vector3 pacmanPosition, Vector3 blinkyPosition)
    {
        Vector3 ghostPosition = ghostController.transform.position;
        // Calculate vector from Pac-Man to Inky and from Blinky to Inky
        Vector3 fromPacman = ghostPosition - pacmanPosition;
        Vector3 fromBlinky = ghostPosition - blinkyPosition;

        // Use the average of the two vectors to find a direction
        Vector3 averageDirection = (fromPacman + fromBlinky).normalized;

        // Decide on a random factor to add some unpredictability
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        // Calculate the escape point by extending the average direction and adding a random offset
        Vector3 escapePoint = ghostPosition + averageDirection * Random.Range(5f, 10f) + randomOffset;

        return escapePoint;
    }
}