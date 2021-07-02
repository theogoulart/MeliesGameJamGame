using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Over! Press 'return' to reload level");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Game paused");
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Game paused");
        }
    }

    public void RestartLevel()
    {
        var level = PlayerPrefs.GetString("current_level", "1");
        SceneManager.LoadScene("Level " + level);
    }

    public void LoadMainMenu()
    {
        Debug.Log("main");
        SceneManager.LoadScene("MainMenu");
    }
}
