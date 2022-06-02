using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthCloser : MonoBehaviour
{
    private bool firstGum = true;
    private BubbleGum bubbleGum;

    private void Awake()
    {
        bubbleGum = FindObjectOfType<BubbleGum>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (firstGum)
        {
            print(firstGum);
            firstGum = false;
            StartCoroutine(CountToCloseTheMouth());
        }
        else
        {
            print("sec = ");
        }
    }

    private IEnumerator CountToCloseTheMouth()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(bubbleGum.GrowBubbleGum());
    }
}
