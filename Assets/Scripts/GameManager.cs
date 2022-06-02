using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Inputs
    private int currentScene;
    private int nextSceneToLoad;
    private int currentLevel;

    // GameObject & Transforms
    public GameObject winPanel, losePanel;
    public GameObject winParticle;

    // Bool & String
    public bool gameIsStarted;
    private MyTerrainEditor terrainEditor;
    private CamCam camCam;

    private void Awake()
    {
        camCam = FindObjectOfType<CamCam>();
        terrainEditor = FindObjectOfType<MyTerrainEditor>();
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            currentLevel = PlayerPrefs.GetInt("LevelNumber");
        }
        else
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("LevelNumber", currentLevel);
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetTheGame();
        }    
    }

    public void ResetTheGame()
    {
        if (terrainEditor)
        {
            terrainEditor.ResetTerrainHeights();
        }
        SceneManager.LoadScene(currentScene);
    }

    public void PlayerWins()
    {
        gameIsStarted = false;
        winPanel.SetActive(true);
    }

    public void NextLevel()
    {
        if (terrainEditor)
        {
            terrainEditor.ResetTerrainHeights();
        }
        currentLevel += 1;
        PlayerPrefs.SetInt("LevelNumber", currentLevel);
        if (nextSceneToLoad > SceneManager.sceneCount-1)
        {
            nextSceneToLoad = 0;
        }
        SceneManager.LoadScene(nextSceneToLoad);
        PlayerPrefs.Save();
    }

    IEnumerator ShowWinningObjects()
    {
        yield return new WaitForSeconds(1);
        winParticle.SetActive(true);
        yield return new WaitForSeconds(1);
        winPanel.SetActive(true);
    }

    public IEnumerator ShowLosingObjects()
    {
        yield return new WaitForSeconds(1);
        losePanel.SetActive(true);
    }
}
