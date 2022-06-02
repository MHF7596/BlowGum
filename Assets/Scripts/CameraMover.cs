using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    public bool following;

    private void Start()
    {
        following = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (following)
        {
            if (other.transform.position.z + 0.5f > transform.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, other.transform.position.z + 0.5f), Time.deltaTime * cameraSpeed);
            }
        }
    }
}
