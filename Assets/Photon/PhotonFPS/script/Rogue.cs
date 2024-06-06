﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : FPSMovement
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    public float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLenght;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private FPSMovement trepar;

    void Awake()
    {
        trepar = GetComponent<FPSMovement>();
    }


    private void Update()
    {
        base.Update();

        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            Debug.Log("detecto pared");
            if (!climbing /*&& climbTimer > 0*/) StartClimbing();

            //futuro c�digo, hay que buscar la manera de resetear el climbing cuando est� grounded
            /*
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
            */
        }

        else
        {
            if (climbing) StopClimbing();
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLenght, whatIsWall);
        Debug.Log(wallFront);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement()
    {
        Debug.Log("Deberia trepar");
        //rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        //trepar.Climbing(climbSpeed);
        // Climbing(climbSpeed);
    }

    private void StopClimbing()
    {
        climbing = false;
    }

    /*public void Climbing(float velocidad)
    {
        moveDirection.y = velocidad;
    }
    */
}

