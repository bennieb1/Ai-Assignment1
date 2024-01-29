using UnityEngine;

public class InkyChasesState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public string GoToDieState = "Dead";
    private int gotoDieStateHash;

    public GameObject Blinky; // Reference to Blinky to calculate target position

    public override void Init(GameObject _owner, fsm _fsm)
    {

        base.Init(_owner, _fsm);

        Blinky = GameObject.Find("Blinky");

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

        Vector3 targetPosition = CalculateInkyTargetPosition();
        ghostController.SetMoveToLocation(targetPosition);

        if (GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoRunawayStateHash);
        }
    }

    private Vector3 CalculateInkyTargetPosition()
    {
        Vector3 pacmanPosition = ghostController.PacMan.transform.position;
        Vector3 blinkyPosition = Blinky.transform.position;

        // Calculate Inky's target position based on Pac-Man and Blinky's positions
        // The actual logic might depend on your game's specific mechanics
        Vector3 targetPosition = pacmanPosition  - blinkyPosition; // Example logic

        return targetPosition;
    }
}