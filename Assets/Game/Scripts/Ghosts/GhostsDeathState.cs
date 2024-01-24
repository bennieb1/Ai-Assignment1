using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class GhostsDeathState : GhostBaseState
{
    public string RespawnStateName = "Respawn";
    private int respawnStateHash;

    public float deathDuration = 5.0f; // Time to respawn
    private float currentTimer = 0;

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        respawnStateHash = Animator.StringToHash(RespawnStateName);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I've been eaten! Going back to the ghost house.");
        currentTimer = deathDuration;

        // Assuming GhostController has a method to handle movement back to the ghost house
        ghostController.MoveToGhostHouse();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            fsm.ChangeState(respawnStateHash);
        }

        // If the ghost reaches the ghost house before the timer expires, you can also trigger the respawn
        if (ghostController.IsAtGhostHouse())
        {
            fsm.ChangeState(respawnStateHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Respawning now.");
        //ghostController.Respawn(); // Assuming GhostController has a method to handle respawn
    }
}
