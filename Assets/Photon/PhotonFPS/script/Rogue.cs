using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rogue : FPSMovement
{
    public Camera Cam;
    public float climbSpeed = 5f;
    public float wallDetectionDistance = 1f;

    private PhotonView photonView;
    private Rigidbody rb;
    private bool isClimbing = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        Debug.Log("Awake: Componentes inicializados");


        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            HandleClimbing();
        }
    }

    private void HandleClimbing()
    {
        bool nearWall = IsNearWall();

        if (nearWall && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
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

        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        bool hitDetected = Physics.Raycast(origin, direction, out hit, wallDetectionDistance);

        Debug.DrawRay(origin, direction * wallDetectionDistance, hitDetected ? Color.green : Color.red);

        return hitDetected;
    }

    private void StartClimbing()
    {
        if (!isClimbing)
        {
            isClimbing = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void StopClimbing()
    {
        if (isClimbing)
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }

    private void Climb()
    {
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f; // Subir
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f; // Bajar
        }

        Vector3 climbMovement = new Vector3(0, verticalInput * climbSpeed * Time.deltaTime, 0);
        transform.Translate(climbMovement);
    }
}

