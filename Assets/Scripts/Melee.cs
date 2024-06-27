using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : FPSMovement
{
    private bool isBoosting = false;
    private float boostDuration = 1f;
    private float boostStartTime;
    private float originalSpeed;
    private Vector3 originalDirection;
    private float playerRadius = 1f; // Adjust based on your player's size

    void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && !isBoosting)
        {
            StartBoost();
        }

        if (isBoosting)
        {
            float elapsed = Time.time - boostStartTime;
            if (elapsed < boostDuration)
            {
                BoostPlayer();
            }
            else
            {
                StopBoost();
            }
        }
    }

    void StartBoost()
    {
        isBoosting = true;
        originalSpeed = movementSpeed;
        movementSpeed *= 2f;
        originalDirection = playerCamera.transform.forward;
        boostStartTime = Time.time;
    }

    void BoostPlayer()
    {
        Vector3 moveDirection = new Vector3(originalDirection.x, 0f, originalDirection.z).normalized;
        myCC.Move(moveDirection * movementSpeed * Time.deltaTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Destructable"))
            {
                Debug.Log("Destroyed: " + hitCollider.gameObject.name);
                Destroy(hitCollider.gameObject);
            }
        }
    }

    void StopBoost()
    {
        isBoosting = false;
        movementSpeed = originalSpeed;
    }
}


