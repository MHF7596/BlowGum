using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCam : MonoBehaviour
{
    [SerializeField] private Transform lastCamera, gameplayCamera, machineCamera;
    [SerializeField] private float cameraSpeed;
    private GameManager gameManager;
    [SerializeField] private Animator gumMachineAnimator;
    [SerializeField] private GameObject theWall;
    [SerializeField] private Transform gumsForGame;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           StartTheGame();
        }
    }

    public IEnumerator GoToLastCamera()
    {
        Invoke(nameof(ActivateTheWall),2);
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, lastCamera.position, Time.deltaTime * cameraSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, lastCamera.rotation, Time.deltaTime * cameraSpeed);
            yield return null;
        }
    }

    private void ActivateTheWall()
    {
        theWall.SetActive(true);
    }

    public IEnumerator GoToGameplayCamera()
    {
        // Girl to Machine
        while (Vector3.Distance(transform.position, machineCamera.position) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, machineCamera.position, Time.deltaTime * cameraSpeed*2);
            transform.rotation = Quaternion.Slerp(transform.rotation, machineCamera.rotation, Time.deltaTime * cameraSpeed*2);
            yield return null;
        }

        gameManager.gameIsStarted = true;
        gumMachineAnimator.SetTrigger("TurnOnRotator");

        // machine to gameplay
        yield return new WaitForSeconds(1);

        while (Vector3.Distance(transform.eulerAngles, gameplayCamera.eulerAngles) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, gameplayCamera.position, Time.deltaTime * cameraSpeed*3);
            transform.rotation = Quaternion.Slerp(transform.rotation, gameplayCamera.rotation, Time.deltaTime * cameraSpeed*3);
            yield return null;
        }

        gumsForGame.SetParent(null);
        gumMachineAnimator.gameObject.SetActive(false);
    }

    public void StartTheGame()
    {
        StartCoroutine(GoToGameplayCamera());
    }
}
