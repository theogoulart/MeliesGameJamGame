using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public string enemyColor;
    private Rigidbody2D rig;
    
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        transform.position = Vector2.MoveTowards(rig.position, player.position, speed * Time.deltaTime);
    }
}
