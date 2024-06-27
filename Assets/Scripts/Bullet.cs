using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public int bulletDamage = 20; // Damage dealt by the bullet

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy");
            // Only apply damage if we are the owner of the bullet
            if (photonView.IsMine)
            {
                // Apply damage to the enemy
                collision.gameObject.GetComponent<EnemyLife>().TakeDamage(bulletDamage);
                // Notify other clients about the damage
                photonView.RPC("ApplyDamage", RpcTarget.Others, collision.gameObject.GetComponent<PhotonView>().ViewID, bulletDamage);
            }
        }
        // Destroy the bullet regardless of what it collides with
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Only apply damage if we are the owner of the bullet
            if (photonView.IsMine)
            {
                // Apply damage to the enemy
                other.gameObject.GetComponent<EnemyLife>().TakeDamage(bulletDamage);
                // Notify other clients about the damage
                photonView.RPC("ApplyDamage", RpcTarget.Others, other.gameObject.GetComponent<PhotonView>().ViewID, bulletDamage);
            }
        }
    }

    [PunRPC]
    void ApplyDamage(int viewID, int damage)
    {
        PhotonView target = PhotonView.Find(viewID);
        if (target != null && target.gameObject != null)
        {
            target.gameObject.GetComponent<EnemyLife>().TakeDamage(damage);
        }
    }
}
