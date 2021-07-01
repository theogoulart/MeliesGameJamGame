using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Press 'return' to start the game");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Return)) {
        //     LoadNewGame();
        // }
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadContinue()
    {
        var level = PlayerPrefs.GetString("current_level", "Level 2");
        SceneManager.LoadScene(level);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
