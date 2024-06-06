using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public GameObject Player;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            Debug.Log("le hiso daño");
            Player.GetComponent<Mage>().health -= damage;
        }
    }
}
