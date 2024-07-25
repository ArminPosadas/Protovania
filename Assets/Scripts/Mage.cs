using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Mage : FPSMovement
{
    public float grabDistance = 2f;
    public LayerMask grabbableLayer;
    private Transform grabbedObject;
    private bool isGrabbing = false;

    private void Update()
    {
        base.Update();
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isGrabbing)
                {
                    TryGrabObject();
                }
                else
                {
                    ReleaseObject();
                }
            }

            if (isGrabbing && grabbedObject != null)
            {
                UpdateGrabbedObjectPosition();
            }
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabDistance, grabbableLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                grabbedObject = rb.transform;
                isGrabbing = true;

                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
            }

            grabbedObject = null;
            isGrabbing = false;
        }
    }

    void UpdateGrabbedObjectPosition()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;

        grabbedObject.position = Vector3.Lerp(grabbedObject.position, targetPosition, Time.deltaTime * 10f);
    }
}
