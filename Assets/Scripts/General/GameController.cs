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
        //enemy = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        
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

}
