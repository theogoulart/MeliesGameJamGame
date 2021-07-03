using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource startSfx;
    public GameObject animation;
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
        StartCoroutine(StartGame("Level 1"));
    }

    public void LoadContinue()
    {
        var level = PlayerPrefs.GetString("current_level", "1");
        StartCoroutine(StartGame("Level " + level));
    }

    public void LoadMainMenu()
    {
        Debug.Log("main");
        StartCoroutine(StartGame("MainMenu"));
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    IEnumerator StartGame(string level)
    {
        startSfx.Play();
        animation.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
