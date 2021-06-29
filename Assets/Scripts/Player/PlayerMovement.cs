using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rig;
    private Vector3 _direction;
    private bool isMovementLocked;

    [SerializeField] private float speed;

    public Vector3 direction
    { 
        get { return _direction; }
        set { _direction = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementLocked) {
            return;
        }

        _direction = Vector3.zero;
        OnInput();
    }

    private void FixedUpdate() {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.deltaTime);
    }

    void OnInput()
    {
        Debug.Log(Input.GetButtonDown("Vertical"));
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("up");
            isMovementLocked = true;
            _direction = Vector3.forward;
            StartCoroutine(UnlockMovements());
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("down");
            isMovementLocked = true;
            _direction = Vector3.back;
            StartCoroutine(UnlockMovements());
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("left");
            isMovementLocked = true;
            _direction = Vector3.left;
            StartCoroutine(UnlockMovements());
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("right");
            isMovementLocked = true;
            _direction = Vector3.right;
            StartCoroutine(UnlockMovements());
        }
    }

    IEnumerator UnlockMovements()
    {
        yield return new WaitForSeconds(.5f);
        isMovementLocked = false;
    }
}
