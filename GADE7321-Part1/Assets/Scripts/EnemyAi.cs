using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Patrol,
    Chase,
    FlagRetrieve,
    PowerUp,
    Return
}

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform enemyflag; // Reference to their flag's transform(Blue Flag)
    public Transform playerFlag;// reference to the enemy flag(Red Flag)
    public Transform playerBase;
    public Transform enemyBase;
    public float visionRange = 10f; // Range within which the enemy can spot the player or the flag
    public float patrolSpeed = 2f; // Speed of patrolling
    public float chaseSpeed = 5f; // Speed of chasing
    public bool flagHeld = false;
    private EnemyState currentState = EnemyState.Patrol;
    private Vector3 initialPosition;
    public bool powerUpHeld = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.FlagRetrieve:
                FlagRetrieve();
                break;
            //EnemyState.PowerUp:
                //usePowerUp();
                //break;
            case EnemyState.Return:
                ReturnToInitialPosition();
                break;
        }
    }

    private void Patrol()
    {
        Debug.Log("Patrol State");
        transform.Translate(Vector3.forward * (patrolSpeed * Time.deltaTime));

        // Check for transitions
        if (CanSeePlayer())
        {
            currentState = EnemyState.Chase;
        }
        else if (CanSeeFlag())
        {
            currentState = EnemyState.FlagRetrieve;
        }
    }

    private void Chase()
    {
        Debug.Log("Chase State");
        if (playerFlag.position == player.position)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * (chaseSpeed * Time.deltaTime));
        }
        // Check for transitions
        if (!CanSeePlayer())
        {
            currentState = EnemyState.Patrol;
        }
    }

    private void FlagRetrieve()
    {
        Debug.Log("Retrieve State");
        if (Vector3.Distance(transform.position, enemyflag.position) < visionRange || enemyflag.position == playerBase.position)
        {
            Vector3 direction = (enemyflag.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        if(Vector3.Distance(transform.position, playerFlag.position) < visionRange && playerFlag.position != enemyBase.position)
        {
            Vector3 direction = (playerFlag.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
        // Check for transitions
        if (!CanSeeFlag())
        {
            Debug.Log("Patrol State");
            currentState = EnemyState.Patrol;
        }
    }

    private void ReturnToInitialPosition()
    {
        Debug.Log("Initial State");
        // Example: Move towards the initial position
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * (patrolSpeed * Time.deltaTime));

        // Check if reached initial position
        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            Debug.Log("Patrol State");
            currentState = EnemyState.Patrol;
        }
    }

    private bool CanSeePlayer()
    {
        // Check if the player is within the vision range
        return Vector3.Distance(transform.position, player.position) <= visionRange;
    }

    private bool CanSeeFlag()
    {
        
        if (Vector3.Distance(transform.position, playerFlag.position) <= visionRange  && playerFlag.position != enemyBase.position)
        {
            return Vector3.Distance(transform.position, playerFlag.position) <= visionRange;
        }
        else
        {
            return false;
        }
        // Check if the flag is within the vision range
        
        
    }

    private void usePowerups()
    {
        
    }
}

