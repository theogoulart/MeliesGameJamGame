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
        PlayerPrefs.SetString("current_level", "1");
        SceneManager.LoadScene("Level 1");
    }

    public void LoadContinue()
    {
        var level = PlayerPrefs.GetString("current_level", "1");
        SceneManager.LoadScene("Level " + level);
    }

    public void LoadMainMenu()
    {
        Debug.Log("main");
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
