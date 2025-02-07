using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject nextWaveTimer;
    TextMeshProUGUI nextWaveTimerText;

    [Header("Game Properties")]
    [SerializeField] float buildPhaseDuration = 5;

    bool isPaused = false;
    bool isBuildPhase = true;
    float previousTimeScale = 1;

    void Awake()
    {
        //Ensure singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        isPaused = false;
        previousTimeScale = 1;
    }

    private void Start()
    {
        if (nextWaveTimer != null)
        {
            nextWaveTimerText = nextWaveTimer.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (isBuildPhase)
        {
            StartCoroutine(BuildPhaseCoroutine());
        }
    }

    private void OnEnable()
    {
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath += GameOver;
        }

        if (WaveManager.instance != null)
        {
            WaveManager.instance.onWaveFinished += StartBuildPhase;
            WaveManager.instance.onLastWaveFinished += GameWon;
        }
    }

    private void OnDisable()
    {
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath -= GameOver;
        }

        if (WaveManager.instance != null)
        {
            WaveManager.instance.onWaveFinished -= StartBuildPhase;
            WaveManager.instance.onLastWaveFinished -= GameWon;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
    }

    void StartBuildPhase()
    {
        if (!isBuildPhase)
        {
            StartCoroutine(BuildPhaseCoroutine());
        }
    }

    IEnumerator BuildPhaseCoroutine()
    {
        isBuildPhase = true;

        for(int i = 0; i < buildPhaseDuration; i++)
        {
            if (nextWaveTimerText != null)
            {
                nextWaveTimerText.text = "Next Wave In: " + (buildPhaseDuration - i).ToString();
            }

            yield return new WaitForSeconds(1);
        }

        nextWaveTimerText.text = "Wave In Progress...";

        WaveManager.instance.StartNextWave();
        isBuildPhase = false;

        yield break;
    }

    void TogglePauseMenu()
    {
        if (pauseMenu == null)
        {
            return;
        }

        if (!isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = previousTimeScale;
        }
    }

    public void GoToFirstScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void GameWon()
    {
        Debug.Log("Game Won");
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
