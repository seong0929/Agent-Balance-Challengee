using UnityEngine;
using UnityEngine.InputSystem;

public class AgentController : MonoBehaviour
{
    public float movementSpeed = 5f;   // ������Ʈ�� �̵� �ӵ�

    private Rigidbody rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // �Է��� ������� �������� �����մϴ�.
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        movement *= movementSpeed;
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}