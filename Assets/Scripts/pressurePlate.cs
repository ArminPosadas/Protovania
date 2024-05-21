using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    private bool playerOnPlate = false;
    
    
    public bool onPlate = false;

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            
            playerOnPlate = true;
            Debug.Log("The player is on top of the pressure plate.");

            onPlate = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerOnPlate = false;
            Debug.Log("The player has left the pressure plate.");
        }
    }

}
