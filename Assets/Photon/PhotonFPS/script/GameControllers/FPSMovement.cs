using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class FPSMovement : MonoBehaviourPun
{
    private PhotonView PV;
    public CharacterController myCC;
    public float movementSpeed = 5f;
    public float sprintSpeed = 8f;
    public float mouseSensitivity = 100f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Vector3 velocity;
    private bool isGrounded;
    public int health = 20;
    public Text healthText;
    private bool isInGhostMode = false;

    public Camera playerCamera;
    private float xRotation = 0f;

    public GameObject ghostTriggerAreaPrefab;
    private GameObject ghostTriggerArea;

    private Weapon playerWeapon;

    public void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();

        if (PV.IsMine)
        {
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Find the HealthText in the scene and assign it
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            UpdateHealthText();

            // Find the Weapon script attached to the player
            playerWeapon = GetComponentInChildren<Weapon>();
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

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : movementSpeed;

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        myCC.Move(move * currentSpeed * Time.deltaTime);
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
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

    private void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine && other.CompareTag("Damager"))
        {
            PV.RPC("TakeDamage", RpcTarget.All, 1);
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        if (PV.IsMine)
        {
            UpdateHealthText();
        }

        if (health <= 0)
        {
            GhostMode();
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

    private void GhostMode()
    {
        Debug.Log("Ghost mode activated");
        isInGhostMode = true;

        if (PV.IsMine)
        {
            // Create trigger area around the player
            ghostTriggerArea = Instantiate(ghostTriggerAreaPrefab, transform.position, Quaternion.identity);
            ghostTriggerArea.transform.SetParent(transform);

            // Disable the weapon
            playerWeapon.enabled = false;
        }
    }

    private void DisableGhostMode()
    {
        Debug.Log("Ghost mode deactivated");
        isInGhostMode = false;

        if (ghostTriggerArea != null)
        {
            Destroy(ghostTriggerArea);
        }

        health = 5;  // Restoring health to 5
        UpdateHealthText();

        // Enable the weapon
        playerWeapon.enabled = true;
    }

    [PunRPC]
    private void RestoreHealth()
    {
        health = 5;
        UpdateHealthText();
        DisableGhostMode();
    }

    public void OnTriggerStay(Collider other)
    {
        if (isInGhostMode && PV.IsMine)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("revivestatue") && health == 0)
            {
                Debug.Log("Collided with Revive Statue - Restoring Health");
                RestorePlayerHealth();
            }
        }
    }

    private void RestorePlayerHealth()
    {
        health += 10;
        if (health > 20) health = 20;
        UpdateHealthText();
        DisableGhostMode();
    }

    [PunRPC]
    public void TeleportPlayer(Vector3 position)
    {
        // Teleport the player to the specified position
        transform.position = position;
    }
}
