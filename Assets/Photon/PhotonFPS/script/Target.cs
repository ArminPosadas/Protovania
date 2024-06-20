using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float climb = 2f;

    public void Commence (float amount)
    {
        climb -= amount;
        if (climb <= 0f)
        {
            Go();
        }
    }

    void Go ()
    {
        Destroy(gameObject);
    }
}
