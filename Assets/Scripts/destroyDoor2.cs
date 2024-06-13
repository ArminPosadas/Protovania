using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyDoor2 : MonoBehaviour
{

    public bool vivos1 = true;
    public bool vivos2 = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("las variables .. " + vivos1 + "y .. " + vivos2);
        if (!vivos1 && !vivos2)
        {
            Destroy(gameObject);
        }


    }

}

