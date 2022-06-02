using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum : MonoBehaviour
{
    [SerializeField] private float growSpeed, eyesSpeed;
    private MeshRenderer meshRenderer;
    [SerializeField] private Transform maskGum;
    [SerializeField] private Material eyesMaterial;
    private float eyeOffset;
    [SerializeField] private Animator faceAnimator;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        eyesMaterial.SetTextureOffset("_BaseMap", Vector2.zero);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(GrowBubbleGum());
        }
    }

    public void OpenYourMouth()
    {
        faceAnimator.SetTrigger("OpenMouth");
    }

    public IEnumerator GrowBubbleGum()
    {
        faceAnimator.SetTrigger("ReadyToChew");
        gameManager.gameIsStarted = false;
        yield return new WaitForSeconds(2);
        faceAnimator.SetTrigger("Bubbling");
        Vector3 target = Vector3.one * 7.5f;
        while (Vector3.Distance(transform.localScale,target) > 0.5f)
        {
            eyeOffset -= Time.deltaTime * eyesSpeed;
            eyesMaterial.SetTextureOffset("_BaseMap", Vector2.up * eyeOffset);
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * growSpeed);
            yield return null;
        }

        faceAnimator.SetTrigger("Blink");
        meshRenderer.enabled = false;
        maskGum.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        gameManager.PlayerWins();
    }
}
