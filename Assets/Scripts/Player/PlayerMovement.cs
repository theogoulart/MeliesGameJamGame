using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rig;
    private Vector3 _direction;
    private Vector3 _nextStep;
    private Vector3 _currentPosition;
    private bool isMovementLocked;
    public LayerMask stepOnLayer;
    public LayerMask finishLayer;

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
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
    }

    private void FixedUpdate() {
        // Move();
    }

    void OnInput()
    {
        if (isMovementLocked) {
            return;
        }

        bool hasMovementKeyBeenPressed = false;

        if (Input.GetKeyDown(KeyCode.W)) {
            _direction = Vector3.forward;
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            _direction = Vector3.back;
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            _direction = Vector3.left;
            hasMovementKeyBeenPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            _direction = Vector3.right;
            hasMovementKeyBeenPressed = true;
        }

        if (hasMovementKeyBeenPressed) {
            RaycastHit hit;
            Physics.SphereCast(transform.position, 1f, _direction, out hit, 3f, stepOnLayer);

            if (hit.collider != null) {
                _nextStep = (_direction * GameController.instance.gridSize) + rig.position;
                StartCoroutine(CallEnemiesTurn());
                StartCoroutine(Move());
                return;
            }

            Physics.SphereCast(transform.position, 1f, _direction, out hit, 3f, finishLayer);
            if (hit.collider != null) {
                _nextStep = (_direction * GameController.instance.gridSize) + rig.position;
                StartCoroutine(Move());
                GameController.instance.FinishLevel();
            }
        }
    }

    IEnumerator Move()
    {
        if (isMovementLocked) {
            yield break;
        }

        isMovementLocked = true;
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

    IEnumerator CallEnemiesTurn()
    {
        yield return new WaitForSeconds(.25f);
        GameController.instance.StartEnemiesTurn();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color32(255, 120, 20, 255);
        Gizmos.DrawWireSphere(transform.position + Vector3.left * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.back * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * 3f, 1f);
    }
}
