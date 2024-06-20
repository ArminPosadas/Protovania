using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rogue : FPSMovement
{
    public Camera Cam;
    public float climbSpeed = 5f; // Velocidad de escalada
    public float wallDetectionDistance = 1f; // Distancia para detectar paredes

    private PhotonView photonView;
    private Rigidbody rb;
    private bool isClimbing = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        Debug.Log("Awake: Componentes inicializados");
    }

    private void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            Debug.Log("Update: PhotonView es mío");
            HandleClimbing();
        }
    }

    private void HandleClimbing()
    {
        bool nearWall = IsNearWall();
        Debug.Log("HandleClimbing: nearWall = " + nearWall);

        if (nearWall && Input.GetKey(KeyCode.W)) // Presiona 'W' para escalar
        {
            StartClimbing();
        }
        else
        {
            StopClimbing();
        }

        if (isClimbing)
        {
            Climb();
        }
    }

    private bool IsNearWall()
    {
        // Lanzar un rayo al frente del personaje para detectar la pared
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        bool hitDetected = Physics.Raycast(origin, direction, out hit, wallDetectionDistance);

        Debug.DrawRay(origin, direction * wallDetectionDistance, hitDetected ? Color.green : Color.red);

        if (hitDetected)
        {
            Debug.Log("IsNearWall: Wall detected at distance " + hit.distance);
            return true;
        }

        return false;
    }

    private void StartClimbing()
    {
        if (!isClimbing)
        {
            Debug.Log("StartClimbing: Starting to climb");
            isClimbing = true;
            rb.useGravity = false; // Desactivar gravedad mientras escalamos
        }
    }

    private void StopClimbing()
    {
        if (isClimbing)
        {
            Debug.Log("StopClimbing: Stopping climbing");
            isClimbing = false;
            rb.useGravity = true; // Reactivar gravedad cuando dejemos de escalar
        }
    }

    private void Climb()
    {
        // Mover el personaje hacia arriba
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        Debug.Log("Climb: Climbing with velocity " + rb.velocity);
    }
}

