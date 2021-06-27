using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig;
    private Vector2 _direction;

    [SerializeField] private float speed;

    public Vector2 direction
    { 
        get { return _direction; }
        set { _direction = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.deltaTime);
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
