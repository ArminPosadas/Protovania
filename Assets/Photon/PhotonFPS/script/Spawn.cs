using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player2"), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
    }
}
