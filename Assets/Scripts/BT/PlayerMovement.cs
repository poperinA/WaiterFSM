using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField] float speed = 5f;
    private Collider playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "NoCollision"
        if (collision.collider.CompareTag("NoCollision"))
        {
            Physics.IgnoreCollision(playerCollider, collision.collider);
        }
    }
}

