using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] enemy;
    private GameObject _menu;
    private MenuController _menuController;
    private GameObject _player;
    private PlayerMovement _playerMov;
    public string lightColor;
    public bool enemiesTurn;
    public static GameController instance;
    public float gameSpeed = 12;
    public float gridSize = 3;
    public float movementTimeout;
    public bool gamePaused;
    public float timeRemaining = 0;
    public bool timerIsRunning = false;

    public TextMeshProUGUI timerText;
    public bool isGameOver = false;
    private string enemyColorScript;
    
    private bool gameIsRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        movementTimeout = gridSize / gameSpeed;

        _player = GameObject.FindWithTag("Player");
        _playerMov = _player.GetComponent<PlayerMovement>();

        _menu = GameObject.FindWithTag("Menu");
        _menuController = _menu.GetComponent<MenuController>();

       timerText.text = timeRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsRunning == true && isGameOver == false){
            timerIsRunning = true;
        }
        UpdateTimerUI();
    }

    public void StartEnemiesTurn()
    {
        Debug.Log("enemies turn");
        enemiesTurn = true;
        StartCoroutine(FinishEnemiesTurn());
    }

    public void lightColorFunction(string color){
        lightColor = color;
        // Debug.Log(color);
        var i = -1;
        foreach (GameObject obj in enemy)
        {
            i++;
            enemyColorScript = enemy[i].GetComponent<EnemyMovement>().enemyColor;

            if(enemyColorScript == color){
                ShowOrhideEnemy(false, i);
            }else{
                ShowOrhideEnemy(true, i);
            }
        }
    }

    public void ShowOrhideEnemy(bool status, int id){
        enemy[id].SetActive(status);
    }

    IEnumerator FinishEnemiesTurn()
    {
        yield return new WaitForSeconds(movementTimeout);

        if (_playerMov.NoMovementsAvailable()) {
            GameOver();
        }

        enemiesTurn = false;
    }

    public void GameOver()
    {
        gamePaused = true;
        _menuController.ShowGameOverMenu();
    }

    public void FinishLevel()
    {
        gamePaused = true;
        _menuController.ShowWinMenu();
    }

    public void StartLevel()
    {
        gamePaused = false;
    }

    public void NextLevel()
    {
        var level = PlayerPrefs.GetString("current_level", "1");
        var nextLevel = Convert.ToString(int.Parse(level) + 1);

        if (nextLevel == "4") {
            SceneManager.LoadScene("Credits");
            return;
        }

        PlayerPrefs.SetString("current_level", nextLevel);
        gamePaused = false;
        SceneManager.LoadScene("Level " + nextLevel);
    }

    public void UpdateTimerUI(){
        if (GameController.instance.gamePaused) {
            return;
        }

        timeRemaining += Time.deltaTime;
        timerText.text = ""+(int)timeRemaining;
     }
}
