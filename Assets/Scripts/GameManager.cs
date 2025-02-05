using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] GameObject PauseMenu;

    bool isPaused = false;
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
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath += GameOver;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            TogglePauseMenu();
        }
    }

    private void OnDisable()
    {
        if (BaseController.instance != null)
        {
            BaseController.instance.onBaseDeath -= GameOver;
        }
    }

    void TogglePauseMenu()
    {
        if (PauseMenu == null)
        {
            return;
        }

        if (!isPaused)
        {
            PauseMenu.SetActive(true);
            isPaused = true;
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = previousTimeScale;
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
