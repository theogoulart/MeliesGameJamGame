using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rig;
    private Vector3 _direction;
    private Vector3 _nextDirection;
    private Vector3 _nextStep;
    private Vector3 _currentPosition;
    private bool isMovementLocked;
    public LayerMask enemyLayer;
    public LayerMask stepOnLayer;
    public LayerMask finishLayer;
    public Light pointLight;

    public AudioSource dieSfx;
    public AudioSource winSfx;
    public AudioSource changeSfx;

    public Vector3 direction
    { 
        get { return _direction; }
        set { _direction = value; }
    }

    public Vector3 nextStep
    { 
        get { return _nextStep; }
        set { _nextStep = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        _currentPosition = transform.position;

        switch (transform.rotation.y) {
            case 0:
                _direction = Vector3.forward;
                break;
            case 90:
                _direction = Vector3.right;
                break;
            case 180:
                _direction = Vector3.back;
                break;
            case 270:
                _direction = Vector3.left;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gamePaused) {
            return;
        }

        OnInput();
    }

    private void FixedUpdate() {
        // Move();
    }

    public void ChangeColor(int red, int green, int blue)
    {
        changeSfx.Play();
        pointLight.color = new Color32(System.Convert.ToByte(red),System.Convert.ToByte(green),System.Convert.ToByte(blue), System.Convert.ToByte(1));
    }

    void OnInput()
    {
        if (isMovementLocked) {
            return;
        }

        if (ChangeColor()) {
            Debug.Log("oi");
            return;
        }

        bool hasMovementKeyBeenPressed = false;

        if (Input.GetKeyDown(KeyCode.W)) {
            _nextDirection = Vector3.forward;
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            if (_direction == Vector3.forward) {
                _nextDirection = Vector3.back;
            } else if (_direction == Vector3.right) {
                _nextDirection = Vector3.left;
            } else if (_direction == Vector3.left) {
                _nextDirection = Vector3.right;
            } else if (_direction == Vector3.back) {
                _nextDirection = Vector3.forward;
            }
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            if (_direction == Vector3.forward) {
                Debug.Log("forward");
                _nextDirection = Vector3.left;
            } else if (_direction == Vector3.right) {
                Debug.Log("right");
                _nextDirection = Vector3.forward;
            } else if (_direction == Vector3.left) {
                Debug.Log("left");
                _nextDirection = Vector3.back;
            } else if (_direction == Vector3.back) {
                Debug.Log("back");
                _nextDirection = Vector3.right;
            }
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            if (_direction == Vector3.forward) {
                _nextDirection = Vector3.right;
            } else if (_direction == Vector3.right) {
                _nextDirection = Vector3.back;
            } else if (_direction == Vector3.left) {
                _nextDirection = Vector3.forward;
            } else if (_direction == Vector3.back) {
                _nextDirection = Vector3.left;
            }
            hasMovementKeyBeenPressed = true;
        }

        if (hasMovementKeyBeenPressed) {
            Debug.Log("press");
            ValidateAndMove();
        }
    }

    bool ValidateAndMove()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, 1f, _nextDirection, out hit, 3f, enemyLayer);
        if (hit.collider != null) {
            return false;
        }

        Physics.SphereCast(transform.position, 1f, _nextDirection, out hit, 3f, stepOnLayer);

        if (hit.collider != null) {
            _nextStep = (_nextDirection * GameController.instance.gridSize) + rig.position;
            StartCoroutine(CallEnemiesTurn());
            StartCoroutine(Move());
            return true;
        }

        Physics.SphereCast(transform.position, 1f, _nextDirection, out hit, 3f, finishLayer);
        if (hit.collider != null) {
            _nextStep = (_nextDirection * GameController.instance.gridSize) + rig.position;
            StartCoroutine(Move());
            StartCoroutine(CallLevelFinished());
            return true;
        }

        return false;
    }

    bool ChangeColor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) {
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                // Debug.Log("change color 0");
                GameController.instance.lightColorFunction("red");
            } else if(Input.GetKeyDown(KeyCode.Alpha1)){
                // Debug.Log("change color 1");
                GameController.instance.lightColorFunction("green");
            }else if(Input.GetKeyDown(KeyCode.Alpha3)){
                // Debug.Log("change color 1");
                GameController.instance.lightColorFunction("blue");
            }else{
                Debug.Log("no light");
            }

            StartCoroutine(CallEnemiesTurn());
            return true;
        }

        return false;
    }

    public void PlayDeathSound()
    {
        dieSfx.Play();
    }

    public bool NoMovementsAvailable()
    {
        RaycastHit hit;

        Physics.SphereCast(transform.position, 1f, Vector3.right, out hit, 3f, stepOnLayer);
        if (hit.collider != null) {
            Physics.SphereCast(transform.position, 1f, Vector3.right, out hit, 3f, enemyLayer);
            if (hit.collider == null) {
                return false;
            }
        }

        Physics.SphereCast(transform.position, 1f, Vector3.back, out hit, 3f, stepOnLayer);
        if (hit.collider != null) {
            Physics.SphereCast(transform.position, 1f, Vector3.back, out hit, 3f, enemyLayer);
            if (hit.collider == null) {
                return false;
            }
        }

        Physics.SphereCast(transform.position, 1f, Vector3.left, out hit, 3f, stepOnLayer);
        if (hit.collider != null) {
            Physics.SphereCast(transform.position, 1f, Vector3.left, out hit, 3f, enemyLayer);
            if (hit.collider == null) {
                return false;
            }
        }

        Physics.SphereCast(transform.position, 1f, Vector3.forward, out hit, 3f, stepOnLayer);
        if (hit.collider != null) {
            Physics.SphereCast(transform.position, 1f, Vector3.forward, out hit, 3f, enemyLayer);
            if (hit.collider == null) {
                return false;
            }
        }

        return true;
    }

    IEnumerator Move()
    {
        if (isMovementLocked) {
            yield break;
        }

        isMovementLocked = true;
        _direction = _nextDirection;
        RotateBody();
        while (MoveToNextNode(_nextStep)) {
            yield return null;
        }

        yield return new WaitForSeconds(GameController.instance.movementTimeout);
        isMovementLocked = false;
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (rig.position = Vector3.MoveTowards(rig.position, goal, GameController.instance.gameSpeed * Time.deltaTime));
    }

    void RotateBody()
    {
        if (_nextDirection == Vector3.forward) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (_nextDirection == Vector3.right) {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (_nextDirection == Vector3.left) {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        if (_nextDirection == Vector3.back) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    IEnumerator CallEnemiesTurn()
    {
        yield return new WaitForSeconds(.25f);
        GameController.instance.StartEnemiesTurn();
    }

    IEnumerator CallLevelFinished()
    {
        yield return new WaitForSeconds(GameController.instance.movementTimeout);
        GameController.instance.FinishLevel();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color32(255, 120, 20, 255);
        Gizmos.DrawWireSphere(transform.position + Vector3.left * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.back * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * 3f, 1f);
    }
}
