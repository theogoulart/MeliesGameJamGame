using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
