using UnityEngine;
using UnityEngine.InputSystem;

public class AgentController : MonoBehaviour
{
    public float movementSpeed = 5f;   // 에이전트의 이동 속도

    private Rigidbody rb;
    private Vector2 movementInput;
    private RewardSystem rewardSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rewardSystem = GetComponent<RewardSystem>(); // RewardSystem 스크립트를 가져옵니다.
    }

    private void FixedUpdate()
    {
        // 입력을 기반으로 움직임을 적용합니다.
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        movement *= movementSpeed;
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌 이벤트 처리 로직을 구현합니다.
        // 땅과의 충돌이나 다른 객체와의 상호작용을 처리할 수 있습니다.
        rewardSystem.AddReward(0.1f); // 충돌 시 0.1의 보상을 RewardSystem에 전달합니다.
    }

    private void OnMove(InputValue value)   //InputSystem으로 키입력을 받는 함수
    {
        movementInput = value.Get<Vector2>();
    }
}