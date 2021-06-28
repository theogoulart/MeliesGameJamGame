using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject player;
    public string lightColor;
    public static GameController instance;

    private string enemyColorScript;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        
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

}
