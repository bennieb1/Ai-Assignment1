using UnityEngine;

public class GhostToBase : GhostBaseState
{
    public string GoToRespawnState = "Respawn";
    private int gotoRespawnState;

 


    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRespawnState = Animator.StringToHash(GoToRespawnState);
     
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        if (ghostController != null)
        {
            ghostController.SetMoveToLocation(ghostController.ReturnLocation);
        }
       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
        Collider collider = gameObject.GetComponent<Collider>(); // Assuming this script is attached to the ghost GameObject
        if (collider != null)
        {
            collider.enabled = false;
        }
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (ghostController != null)
        {
            ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoRespawnState));
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // Additional code to handle exiting the dead state...
    }
}
