using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : FPSMovement
{
    public float grabDistance = 2f; // Maximum distance for grasping objects
    public LayerMask grabbableLayer; // objects that can be grabbed
    private Transform grabbedObject; // Reference to grabbed object, if any Indicates whether the player is grabbing an object
    private bool isGrabbing = false;  

    void Update()
    {
        base.Update();

        // the player presses the E key to grab or release
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If the player is not grabbing an object, try to grab a new one
            if (!isGrabbing)
            {
                TryGrabObject();
            }
            // If the player is already grabbing an item, let go
            else
            {
                ReleaseObject();
            }
        }
    }

    // Attempts to grab an object if one is available within range
    void TryGrabObject()
    {
        // Launch a beam from the camera forward to detect grabble objects
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabDistance, grabbableLayer))
        {
            // Check if the hit object has a Rigidbody component
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Grab the object by setting its transform as the player's child
                grabbedObject = rb.transform;
                grabbedObject.SetParent(transform);
                isGrabbing = true;
            }
        }
    }

    // Release the currently held object
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // drop the object
            grabbedObject.SetParent(null);
            grabbedObject = null;
            isGrabbing = false;
        }
    }

}
