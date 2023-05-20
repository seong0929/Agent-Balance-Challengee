using UnityEngine;
using UnityEngine.InputSystem;

public class AgentController : MonoBehaviour
{
    public float movementSpeed = 5f;   // ������Ʈ�� �̵� �ӵ�

    private Rigidbody rb;
    private Vector2 movementInput;
    private RewardSystem rewardSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rewardSystem = GetComponent<RewardSystem>(); // RewardSystem ��ũ��Ʈ�� �����ɴϴ�.
    }

    private void FixedUpdate()
    {
        // �Է��� ������� �������� �����մϴ�.
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        movement *= movementSpeed;
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        rewardSystem.AddReward(0.1f); // �浹 �� 0.1�� ������ RewardSystem�� ����
    }

    private void OnMove(InputValue value)   //InputSystem���� Ű�Է��� �޴� �Լ�
    {
        movementInput = value.Get<Vector2>();
    }
}