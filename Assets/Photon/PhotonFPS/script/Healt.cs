using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Healt : MonoBehaviour
{
    public int healt = 100;

    [PunRPC]
    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage called with damage: " + damage);
        healt -= damage;
        Debug.Log("Health after damage: " + healt);

        if (healt <= 0)
        {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}
