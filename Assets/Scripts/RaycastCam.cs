using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    Camera cam;

    [SerializeField] private Transform ring;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeformMesh();
        }
    }

    private void DeformMesh()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            DeformPlane deformPlane = hit.transform.GetComponent<DeformPlane>();
            deformPlane.DeformThisPlane(hit.point);

            Instantiate(ring, hit.point, Quaternion.Euler(-90, 0, 0));
        }
    }
}
