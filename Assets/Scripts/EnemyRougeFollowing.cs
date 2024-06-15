using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRougeFollowing : MonoBehaviour
{


    // Start is called before the first frame update
    public Transform objetivo;
    public float velocidad;
    public NavMeshAgent IA;




    // Update is called once per frame
    void Update()
    {
        IA.speed = velocidad;
        IA.SetDestination(objetivo.position);


        objetivo = GameObject.Find("Rouge(Clone)").transform;

    }
}
