using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturber : MonoBehaviour
{
    [SerializeField] private float disturbForce;
    private void OnTriggerEnter(Collider other)
    {
        if (true)
        {
            float n =(float) Random.Range(-5, 5) / 5;
            other.GetComponent<Rigidbody>().AddForce(Vector3.right * disturbForce * n);
        }
    }
}
