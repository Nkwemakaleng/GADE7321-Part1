using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private GameManager gameManager;
    public bool redFlagTaken = false;
    public bool blueFlagTaken = false;
   [SerializeField] private GameObject blueFlag;
   [SerializeField] private GameObject redFlag;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GetComponent())

        {
            
        }

        if (other.CompareTag("Enemy") && gameManager.redFlag)
        {
            gameManager.RespawnFlags();
        }
    }

    public bool ThisFlagPickedUp(GameManager gameManager)
    {
        
    }
    
}
