using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour
{
    private CameraMover cameraMover;
    private CamCam camCam;
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        camCam = FindObjectOfType<CamCam>();
        cameraMover = FindObjectOfType<CameraMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        cameraMover.following = false;
        //gameObject.SetActive(false);
        StartCoroutine(camCam.GoToLastCamera());
    }
}
