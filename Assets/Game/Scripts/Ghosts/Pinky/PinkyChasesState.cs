using UnityEngine;

public class PinkyChasesState : GhostBaseState
{
	public string GoToRunawayState = "Runaway";
	private int gotoRunawayStateHash;

	public string GoToDieState = "Dead";
	private int gotoDieStateHash;

	private Vector3 offset = new Vector3(4, 0, 4); // Offset for target position

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

		Vector3 targetPosition = CalculatePinkyTargetPosition();
		ghostController.SetMoveToLocation(targetPosition);

		if (GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
		{
			fsm.ChangeState(gotoRunawayStateHash);
		}
	}

	private Vector3 CalculatePinkyTargetPosition()
	{

        Vector3 pacmanPosition = ghostController.PacMan.transform.position;
        Vector3 pacmanDirection = ghostController.PacMan.transform.forward;

        // Offset distance in tiles (e.g., 4 tiles ahead of Pac-Man)
        float offsetDistance = 4.0f;

        Vector3 targetPosition = pacmanPosition + (pacmanDirection * offsetDistance);

        // Implement the original game's bug when Pac-Man is facing upwards
        if (pacmanDirection == Vector3.back) // Assuming Vector3.back represents upwards
        {
            targetPosition += new Vector3(0, 0, -offsetDistance); // Adjust for the bug
        }

        return targetPosition;
    }
}