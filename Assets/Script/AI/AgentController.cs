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
    public void PerformAction(Action action)
    {
        // ������ �ൿ�� ���� ������ ������ �ۼ�
        if (action.actionName == "MoveForward")
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else if (action.actionName == "MoveBackward")
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }
        else if (action.actionName == "MoveLeft")
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
        else if (action.actionName == "MoveRight")
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
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