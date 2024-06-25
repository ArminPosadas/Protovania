using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rogue : FPSMovement
{
    public Camera Cam;
    public float climbSpeed = 5f; // velocidad de escalada
    public float wallDetectionDistance = 1f; // distancia de paredes
    public float edgeDetectionDistance = 1f; //detectar borde
    public string wallTag = "whatIsWall"; // etiqueta de la pared

    private PhotonView photonView;
    private CharacterController cc;
    private bool isClimbing = false;
    private bool atWallEdge = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cc = GetComponent<CharacterController>();
        Debug.Log("Awake: Componentes inicializados");
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

        if (nearWall && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) // w o s para escalar
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
        // rayo enfrente para probar colisiones
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        bool hitDetected = Physics.Raycast(origin, direction, out hit, wallDetectionDistance) && hit.collider.CompareTag(wallTag);

        Debug.DrawRay(origin, direction * wallDetectionDistance, hitDetected ? Color.green : Color.red);

        // verificar si estamos cerca de la pared
        atWallEdge = !Physics.Raycast(origin + Vector3.up * edgeDetectionDistance, direction, wallDetectionDistance, LayerMask.GetMask(wallTag));
        Debug.DrawRay(origin + Vector3.up * edgeDetectionDistance, direction * wallDetectionDistance, atWallEdge ? Color.blue : Color.yellow);

        return hitDetected;
    }

    private void StartClimbing()
    {
        if (!isClimbing)
        {
            isClimbing = true;
            cc.enabled = false; // desactivar CharacterController temporalmente
        }
    }

    private void StopClimbing()
    {
        if (isClimbing)
        {
            isClimbing = false;
            cc.enabled = true; // activar CharacterController
        }
    }

    private void Climb()
    {
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f; // subir
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f; // bajar
        }

        // mover arriba abajo
        Vector3 climbMovement = new Vector3(0, verticalInput * climbSpeed * Time.deltaTime, 0);
        
        // sube si llegas al tope
        if (atWallEdge && verticalInput > 0)
        {
            climbMovement = new Vector3(0, verticalInput * climbSpeed * Time.deltaTime, 0);
            // saltar al llegar al tope
            if (climbMovement.y >= edgeDetectionDistance)
            {
                cc.enabled = true; // reactivar CharacterController
                Vector3 jumpMovement = new Vector3(0, Mathf.Sqrt(jumpForce * 6f * gravity), 0);
                cc.Move(jumpMovement * Time.deltaTime);
                StopClimbing();
            }
        }

        transform.Translate(climbMovement);
    }
}
