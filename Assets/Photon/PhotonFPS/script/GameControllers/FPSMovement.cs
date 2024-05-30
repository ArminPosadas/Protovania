using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]

public class FPSMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;
    public float movementSpeed = 5f;
    public float rotationSpeed = 700f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    public int health = 20;

    public Camera playerCamera;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();

        if (PV.IsMine)
        {

            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(true);
            }
        }
        else
        {

            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (PV.IsMine && PhotonNetwork.IsConnected)
        {
            BasicMovement();
            BasicRotation();
            ApplyGravity();
            Jump();
        }
    }

    void BasicMovement()
    {
        isGrounded = myCC.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        myCC.Move(move * movementSpeed * Time.deltaTime);
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        myCC.Move(velocity * Time.deltaTime);
    }


}
