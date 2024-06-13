using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rogue : FPSMovement, IPunObservable
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
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

    private PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        Debug.Log("Awake: Componentes inicializados");
    }

    private void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            Debug.Log("Update: PhotonView es mío");
            WallCheck();
            StateMachine();

            if (climbing)
            {
                ClimbingMovement();
            }
        }
    }

    private void StateMachine()
    {
        Debug.Log($"StateMachine: wallFront={wallFront}, KeyW={Input.GetKey(KeyCode.W)}, wallLookAngle={wallLookAngle}");
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            Debug.Log("StateMachine: Detecto pared y comenzando a escalar");
            if (!climbing)
                StartClimbing();
        }
        else
        {
            if (climbing)
                StopClimbing();
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        Debug.Log($"WallCheck: wallFront={wallFront}");
        if (wallFront)
        {
            wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
            Debug.Log($"WallCheck: wallLookAngle={wallLookAngle}");
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        climbTimer = maxClimbTime;
        Debug.Log("StartClimbing: Iniciando escalada");
        photonView.RPC("RPC_StartClimbing", RpcTarget.Others);
    }

    private void ClimbingMovement()
    {
        Debug.Log("ClimbingMovement: Trepando");
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);

        climbTimer -= Time.deltaTime;
        if (climbTimer <= 0)
        {
            StopClimbing();
        }
    }

    private void StopClimbing()
    {
        climbing = false;
        Debug.Log("StopClimbing: Deteniendo escalada");
        photonView.RPC("RPC_StopClimbing", RpcTarget.Others);
    }

    [PunRPC]
    private void RPC_StartClimbing()
    {
        climbing = true;
        Debug.Log("RPC_StartClimbing: Iniciado en otro cliente");
    }

    [PunRPC]
    private void RPC_StopClimbing()
    {
        climbing = false;
        Debug.Log("RPC_StopClimbing: Detenido en otro cliente");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Enviar datos
            stream.SendNext(climbing);
        }
        else
        {
            // Recibir datos
            climbing = (bool)stream.ReceiveNext();
        }
    }
}





