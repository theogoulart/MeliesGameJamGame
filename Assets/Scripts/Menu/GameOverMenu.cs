using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
