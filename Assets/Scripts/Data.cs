using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int vidaPlayer;

    // Update is called once per frame
    void Update()
    {
        if (vidaPlayer <= 0)
        {
            Debug.Log("GAME OVER");
        }


    }
}
