using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : FPSMovement
{
    [Header("References")]
    public Transform orientation;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private void Start()
    {
        base.Start();
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
            if (!climbing && climbTimer > 0) StartClimbing();
        }
        else
        {
            if (climbing) StopClimbing();
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        if (wallFront)
        {
            wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        climbTimer = maxClimbTime;
    }

    private void ClimbingMovement()
    {
        climbTimer -= Time.deltaTime;
        if (climbTimer <= 0)
        {
            StopClimbing();
            return;
        }

        velocity.y = climbSpeed; 
        myCC.Move(velocity * Time.deltaTime);
    }

    private void StopClimbing()
    {
        climbing = false;
        velocity.y = 0; 
    }
}

