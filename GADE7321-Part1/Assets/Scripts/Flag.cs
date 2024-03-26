using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private GameManager gameManager;
    public static bool isHeld = false;
    private EnemyAI enemyAI;
    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld && other.CompareTag("Player"))
        {
            isPicked(GameObject.FindWithTag("Player"));
            Debug.Log("Flag picked by player");
        }

        if (!isHeld && other.CompareTag("Enemy"))
        {
            isPicked(GameObject.FindWithTag("Enemy"));
            Debug.Log("Flag picked by enemy");
        }
    }

    private void isDropped()
    {
        isHeld = false;
        transform.parent = null;
    }
    private void isPicked(GameObject entity)
    {
        
        if (GameObject.FindWithTag("Enemy"))
        {
            if (GameObject.FindWithTag("BlueFlag"))
            {
                isHeld = true;
                
                transform.parent = playerController.enemy.transform; 
            }
            else
            {
                transform.position = gameManager.blueBase.position;
            }
        }
        if (GameObject.FindWithTag("Player"))
        {
            if (GameObject.FindWithTag("RedFlag"))
            {
                isHeld = true;
                transform.parent = enemyAI.player.transform; 
            }
            else
            {
                transform.position = gameManager.redBase.position;
            }
        }
    }
}
