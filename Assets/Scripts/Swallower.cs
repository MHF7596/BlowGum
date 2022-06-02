using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (true)
        {
            Destroy(other.gameObject);
        }
    }
}
