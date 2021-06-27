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

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Debug.Log("change color 0");
            } else {
                Debug.Log("change color 1");
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
