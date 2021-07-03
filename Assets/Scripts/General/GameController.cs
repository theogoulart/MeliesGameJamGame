using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject redEnemy;
    private GameObject blueEnemy;
    private GameObject greenEnemy;
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

    public SpriteRenderer redbun;
    public SpriteRenderer bluebun;
    public SpriteRenderer greenbun;
    
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

        redEnemy = GameObject.FindWithTag("RedEnemies");
        greenEnemy = GameObject.FindWithTag("GreenEnemies");
        blueEnemy = GameObject.FindWithTag("BlueEnemies");

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

        switch (lightColor) {
            case "red":
                redEnemy.SetActive(false);
                greenEnemy.SetActive(true);
                blueEnemy.SetActive(true);
                greenbun.color = new Color(255,255,255,0.5f);
                bluebun.color = new Color(255,255,255,0.5f);
                redbun.color = new Color(255,255,255,1);
                _playerMov.ChangeColor(255,78,90);
                break;
            case "green":
                redEnemy.SetActive(true);
                greenEnemy.SetActive(false);
                blueEnemy.SetActive(true);
                greenbun.color = new Color(255,255,255,1);
                bluebun.color = new Color(255,255,255,.5f);
                redbun.color = new Color(255,255,255,.5f);
                _playerMov.ChangeColor(86,255,88);
                break;
            case "blue":
                redEnemy.SetActive(true);
                greenEnemy.SetActive(true);
                blueEnemy.SetActive(false);
                greenbun.color = new Color(255,255,255,.5f);
                bluebun.color = new Color(255,255,255,1);
                redbun.color = new Color(255,255,255,.5f);
                _playerMov.ChangeColor(91,85,255);
                break;
        }
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
