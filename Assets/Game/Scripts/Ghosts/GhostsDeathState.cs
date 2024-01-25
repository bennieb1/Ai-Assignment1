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

    private GhostController ghostController; // Reference to the GhostController

    public override void Init(GameObject _owner, fsm _fsm)
    {
        base.Init(_owner, _fsm);
        respawnStateHash = Animator.StringToHash(RespawnStateName);
        deathStateHash = Animator.StringToHash(DeathStateName);
        ghostHouseLocation = new Vector2(0, 0); // Assuming the ghost house is at the center of the map
        ghostController = _owner.GetComponent<GhostController>(); // Initialize the GhostController reference
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I've been eaten! Going back to the ghost house.");
        
        //isReturningToGhostHouse = true;
        //ghostController.SetMoveToLocation(ghostHouseLocation); // Assuming GhostController has a method to set the target
       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        ghostController.Kill();
        fsm.ChangeState(deathStateHash);
        currentTimer = deathDuration;
        ghostController.SetMoveToLocation(ghostHouseLocation); // Assuming GhostController has a method to set the target
        ghostHouseLocation = new Vector2(0, 0);

        if (isReturningToGhostHouse)
        {

           

            if (Vector2.Distance(ghostController.transform.position, ghostHouseLocation) < 0.1f)
            {
                Respawn();
            }
            else if (currentTimer <= 0)
            {
                Respawn();
            }
        }

        currentTimer -= Time.deltaTime;
    }

    private void Respawn()
    {
        Debug.Log("Respawning now.");
        isReturningToGhostHouse = false;
        ghostController.transform.position = ghostHouseLocation; // Or to a start position
        fsm.ChangeState(respawnStateHash);
    }

    
}
