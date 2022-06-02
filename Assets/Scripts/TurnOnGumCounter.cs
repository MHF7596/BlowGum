using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnGumCounter : MonoBehaviour
{
    [SerializeField] private GameObject gumCounter;
    private Collider _collider;
    private BubbleGum bubbleGum;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        bubbleGum = FindObjectOfType<BubbleGum>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        print("gum counter is on");
        gumCounter.SetActive(true);

        //StartCoroutine(MouthCounter());
    }

    private IEnumerator MouthCounter()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(bubbleGum.GrowBubbleGum());
    }
}
