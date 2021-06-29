using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowPauseMenu();
        }
    }

    void ShowGameOverMenu()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    void ShowPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    void ShowWinMenu()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }

    void LoadMainMenu()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }
}
