using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game paused");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Game paused");
        }
    }

    public void ContinueGameplay()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Debug.Log("main");
        SceneManager.LoadScene("MainMenu");
    }
}
