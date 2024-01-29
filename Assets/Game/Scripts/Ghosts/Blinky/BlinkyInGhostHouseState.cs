using UnityEngine;

public class BlinkyInGhostHouseState : GhostBaseState
{
    public string GotoChaseStateHash = "Chase";
    private int gotoChaseStateHash;
    private float timeInGhostHouse = 0f;
    private float requiredTimeInGhostHouse = 5f; // Example time Blinky spends in ghost house

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GotoChaseStateHash);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Reset time counter when Blinky enters the ghost house
        timeInGhostHouse = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Update the time Blinky has been in the ghost house
        timeInGhostHouse += Time.deltaTime;

        // Check if Blinky has spent enough time in the ghost house
        if (timeInGhostHouse >= requiredTimeInGhostHouse)
        {
            fsm.ChangeState(gotoChaseStateHash);
        }

        // Additional behavior while in the ghost house...
    }
}