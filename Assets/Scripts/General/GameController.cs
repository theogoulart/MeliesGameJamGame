using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject player;
    public string lightColor;
    public bool enemiesTurn;
    public static GameController instance;
    public float gameSpeed = 12;
    public float gridSize = 3;
    public float movementTimeout;

    private string enemyColorScript;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        movementTimeout = gridSize / gameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
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

        for(int i = 0; i < enemy.Length; i++) {
            enemyColorScript = enemy[i].GetComponent<EnemyMovement>().enemyColor;
            Debug.Log(enemyColorScript);
            // if(enemy[i].GetComponent<EnemyMovement>().enemyColor == color){
            //     ShowOrhideEnemy(false);
            // }

            // if(enemy[i].GetComponent<EnemyMovement>().enemyColor != color){
            //     ShowOrhideEnemy(true);
            // }
        }

    

    }

    public void ShowOrhideEnemy(bool status){
        enemy[0].SetActive(status);
    }

    IEnumerator FinishEnemiesTurn()
    {
        yield return new WaitForSeconds(movementTimeout);
        enemiesTurn = false;
    }

    public void FinishLevel()
    {
        Debug.Log("congratulations!");
    }
}
