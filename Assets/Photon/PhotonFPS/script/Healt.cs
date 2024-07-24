using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Healt : MonoBehaviour
{
    public int healt;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        healt -= _damage;

        if (healt <= 8)
        {
            Destroy(gameObject);
        }
    }
}
