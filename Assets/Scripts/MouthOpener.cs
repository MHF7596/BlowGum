using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthOpener : MonoBehaviour
{
    private BubbleGum bubbleGum;

    void Start()
    {
        bubbleGum = FindObjectOfType<BubbleGum>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        bubbleGum.OpenYourMouth();
    }
}
