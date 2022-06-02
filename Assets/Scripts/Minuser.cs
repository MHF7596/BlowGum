using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minuser : MonoBehaviour
{
    [SerializeField] private int numberOfMultiplier;
    [SerializeField] private TMP_Text tMP;
    [SerializeField] private GameObject particle;
    [SerializeField] private string theName;

    private int numberRemain;

    private void Start()
    {
        numberRemain = numberOfMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != theName)
        {
            numberRemain -= 1;
            tMP.text = "-" + numberRemain.ToString();
            Instantiate(particle, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if(numberRemain < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
