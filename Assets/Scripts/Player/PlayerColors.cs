using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColors : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool isCooldownActive;

    enum State {
        Red, Blue, Green
    }

    [SerializeField] private float cooldownSeconds;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        if (isCooldownActive) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                // Debug.Log("change color 0");
                GameController.instance.lightColorFunction("blue");
            } else if(Input.GetKeyDown(KeyCode.Alpha2)){
                // Debug.Log("change color 1");
                GameController.instance.lightColorFunction("green");
            }else if(Input.GetKeyDown(KeyCode.Alpha3)){
                // Debug.Log("change color 1");
                GameController.instance.lightColorFunction("natual");
            }else{
                Debug.Log("no light");
            }

            sprite.color = new Color(1,0,0,1);
            isCooldownActive = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownSeconds);
        isCooldownActive = false;
    }
}
