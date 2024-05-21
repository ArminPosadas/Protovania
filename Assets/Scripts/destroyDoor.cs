using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class destroyDoor : MonoBehaviour
{
    public pressurePlate plate1;
    public pressurePlate plate2;
    public pressurePlate plate3;
    public pressurePlate plate4;


    // Update is called once per frame
    void Update()
    {
        if (plate1.onPlate && plate2.onPlate && plate3.onPlate && plate4.onPlate)
        {
            Destroy(gameObject);
        }

    }

}
