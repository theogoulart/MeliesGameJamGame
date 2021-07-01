using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rig;
    public string enemyColor;
    private bool isMovementLocked;
    private Vector3 _nextStep;
    private Vector3 _direction;
    public LayerMask stepOnLayer;
    public LayerMask enemyLayer;

    [SerializeField] private Transform player;
    private PlayerMovement _playerMov;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        _playerMov = player.GetComponent<PlayerMovement>();
    }

    void Update() {
        Turn();
    }

    void Turn()
    {
        if (GameController.instance.enemiesTurn && !isMovementLocked) {
            Vector3 dir = (_playerMov.nextStep - transform.position).normalized;
            if (dir.x < 0f && dir.x < dir.z) {
                _direction = Vector3.left;
            } else if (dir.x > 0f && dir.x > dir.z) {
                _direction = Vector3.right;
            } else  if (dir.z > 0f) {
                _direction = Vector3.forward;
            } else if (dir.z < 0f) {
                _direction = Vector3.back;
            }

            if (!ValidateAndMove()) {
                if (_direction == Vector3.back || _direction == Vector3.forward) {
                    if (dir.x < 0f) {
                        _direction = Vector3.left;
                    } else {
                        _direction = Vector3.right;
                    }
                } else {
                    if (dir.z < 0f) {
                        _direction = Vector3.back;
                    } else {
                        _direction = Vector3.forward;
                    }
                }

                ValidateAndMove();
            }
        }
    }

    bool ValidateAndMove()
    {
            RaycastHit hit;
            Physics.SphereCast(transform.position, 1f, _direction, out hit, 3f, enemyLayer);
            if (hit.collider != null) {
                return false;
            }

            Physics.SphereCast(transform.position, 1f, _direction, out hit, 3f, stepOnLayer);

            if (hit.collider != null) {
                _nextStep = (_direction * GameController.instance.gridSize) + rig.position;
                StartCoroutine(Move());
                return true;
            }

            return false;
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

    private void OnDrawGizmos() {
        Gizmos.color = new Color32(255, 120, 20, 255);
        Gizmos.DrawWireSphere(transform.position + Vector3.left * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.back * 3f, 1f);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * 3f, 1f);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Enemy")) {
            Debug.Log(other);
        }
    }
}
