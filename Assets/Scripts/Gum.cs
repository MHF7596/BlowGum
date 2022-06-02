using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : MonoBehaviour
{
    private Renderer gumRenderer;
    private Rigidbody gumRb;
    [SerializeField] private float jumpInMouthPower, gumSpeed, fallingPowerY, fallingPowerZ, gumTorquePower;
    [SerializeField] private List<Material> gumMats;
    [SerializeField] private Transform gumPlace;
    private int randomNumber;
    private bool falling;
    private Collider _collider;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    private GameManager gameManager;
    [SerializeField] private GameObject particle;
    private bool go;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        _collider = GetComponent<Collider>();
        gumRenderer = GetComponent<Renderer>();
        gumRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        randomNumber = Random.Range(0, 4);
        gumRenderer.material = gumMats[randomNumber];
    }

    private void FixedUpdate()
    {
        if (gameManager.gameIsStarted)
        {
            if (!go)
            {
                if (!falling)
                {
                    if (!Physics.Raycast(transform.position, Vector3.forward, out hit, 1, layerMask))
                    {
                        gumRb.AddForce(Vector3.forward * gumSpeed);
                        gumRb.AddTorque(Vector3.right * gumTorquePower);
                    }
                    else
                    {
                        gumRb.AddTorque(Vector3.right * gumTorquePower);
                    }
                }
                else
                {
                    gumRb.AddForce(new Vector3(0, -fallingPowerY, fallingPowerZ));
                    gumRb.AddTorque(Vector3.right * gumTorquePower * 10);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jumper"))
        {
            JumpInMouth();
        }
        else if (other.CompareTag("Hole"))
        {
            FallInHole();
        }
        else if (other.CompareTag("Obstacle"))
        {
            Instantiate(particle, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void JumpInMouth()
    {
        //go = true;
        gumRb.constraints = RigidbodyConstraints.None;
        Vector3 dir = gumPlace.position - transform.position;
        gumRb.AddForce(dir.normalized * jumpInMouthPower, ForceMode.Impulse);
    }

    private void FallInHole()
    {
        _collider.enabled = false;
        falling = true;
        gumRb.constraints = RigidbodyConstraints.None;
    }
}
