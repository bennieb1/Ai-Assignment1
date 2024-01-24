using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class GhostsDeathState : GhostBaseState
{

    public string DeathStateName = "IsDead";
    private int deathStateHash;

    public string RespawnStateName = "IsGhost";
    private int respawnStateHash;



    public float deathDuration = 5.0f; // Time to respawn
    private float currentTimer = 0;

    private Vector2 ghostHouseLocation; // Location of the ghost house
    private bool isReturningToGhostHouse = false;


    public override void Init( fsm _fsm, GameObject _owner)
    {
        base.Init(_fsm, _owner);
        respawnStateHash = Animator.StringToHash(RespawnStateName);
        deathStateHash = Animator.StringToHash(DeathStateName);
        ghostHouseLocation = new Vector2(0, 0);// Assuming the ghost house is at the center of the map

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I've been eaten! Going back to the ghost house.");
        fsm.ChangeState(deathStateHash);
        currentTimer = deathDuration;
        isReturningToGhostHouse = true;
        ghostController.SetMoveToLocation(ghostHouseLocation); // Assuming GhostController has a method to set the target


       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isReturningToGhostHouse) 
        {
            // Check if the ghost has reached the ghost house
            if (Vector2.Distance(ghostController.transform.position, ghostHouseLocation) < 0.1f)
            {

                Respawn();
            }
            else if (currentTimer <= 0)
            {
                // Time out - force respawn even if not reached
                Respawn();
            }
        }

        currentTimer -= Time.deltaTime;
    }

    //public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("Respawning now.");
    //    //ghostController.Respawn(); // Assuming GhostController has a method to handle respawn
    //}

    private void Respawn()
    {
        Debug.Log("Respawning now.");
        isReturningToGhostHouse = false;
        // Reset position, animation, or any other properties
        ghostController.transform.position = ghostHouseLocation; // Or to a start position

        fsm.ChangeState(respawnStateHash);
    }

}
