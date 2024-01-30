using UnityEngine;

public class GhostToBase : GhostBaseState
{
    public string GoToRespawnState = "Respawn";
    private int gotoRespawnState;

    public string GoToGhostHouseState = "GhostHouse";
    private int gotoGhostHouseState;

    private int totalDots = 1500; // Hardcoded total dots for a standard level
    private int dotsEatenThreshold;


    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRespawnState = Animator.StringToHash(GoToRespawnState);
        gotoGhostHouseState = Animator.StringToHash(GoToGhostHouseState);
        dotsEatenThreshold = totalDots / 3; // A third of the total dots
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.SetMoveToLocation(ghostController.ReturnLocation);
        }
        if (GameDirector.Instance.ScoreValue >= dotsEatenThreshold)
        {
            fsm.ChangeState(gotoGhostHouseState);
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
