using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusBonus : MonoBehaviour
{
    [SerializeField] private int numberOfMultiplier;
    [SerializeField] private TMP_Text tMP;
    private int numberRemain;
    [SerializeField] private string theName;
    private void Start()
    {
        numberRemain = numberOfMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != theName)
        {
            numberRemain -= 1;
            tMP.text = "+" + numberRemain.ToString();
            GameObject gameObject = Instantiate(other.gameObject, other.transform.position, other.transform.rotation);
            gameObject.name = theName;
        }
        other.name = theName;

        if (numberRemain < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
