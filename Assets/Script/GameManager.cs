using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;   // AgentController 스크립트를 가진 에이전트를 참조합니다.
    public Transform respawnPoint;  // 리스폰 지점을 설정합니다.
    public TMP_Text timerText;      // UI에 타이머를 표시할 TMP_Text 오브젝트를 연결합니다.

    private float timer = 0f;       // 게임 타이머를 저장합니다.

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // 게임이 실행 중인지 확인합니다.
        if (agent.enabled)
        {
            timer += Time.deltaTime;

            // 타이머 값을 UI에 표시합니다.
            UpdateTimerUI();

            // 게임 종료 조건을 확인합니다.
            if (agent.transform.position.y < -10f)  // 에이전트가 땅보다 완전히 아래에 있을 때
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // 게임 시작 시 초기화 작업을 수행합니다.
        timer = 0f;

        // 에이전트를 리스폰 지점으로 이동시킵니다.
        agent.transform.position = respawnPoint.position;

        // 에이전트를 활성화합니다.
        agent.enabled = true;
    }

    public void EndGame()
    {
        // 게임 종료 시 처리할 작업을 수행합니다.

        // 에이전트를 비활성화합니다.
        agent.enabled = false;

        // 게임 종료에 대한 추가 로직을 구현합니다.

        // 예를 들어, 최종 점수를 계산하거나 결과를 표시할 수 있습니다.
    }

    private void UpdateTimerUI()
    {
        // UI에 타이머 값을 표시합니다.
        timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
    }
}