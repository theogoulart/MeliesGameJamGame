using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("You win! Press 'Enter' to go to the next level!");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Next level");
        }
    }

    void GoToNextLevel()
    {
        GameController.instance.NextLevel();
    }
}
