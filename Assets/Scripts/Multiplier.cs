using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private int numberOfMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < numberOfMultiplier - 1; i++)
        {
            GameObject gameObject = Instantiate(other.gameObject, other.transform.position, other.transform.rotation);
        }

        gameObject.SetActive(false);
    }
}
