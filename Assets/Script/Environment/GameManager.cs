using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;    // 에이전트를 참조
    public Transform respawnPoint;   // 리스폰 지점을 설정
    public TMP_Text timerText;       // 타이머 UI
    public TMP_Text rewardText;      // 보상 UI
    public TMP_Text timeLimitText;   // 제한 시간 UI
    public int currentEpisode = 0;   // 현재 에피소드 수
    public int maxEpisodes = 100;    // 최대 에피소드 수
    public float timeLimit = 180f;   // 시간 제한 (초 단위)

    private Dictionary<StateActionPair, float> qTable;  // Q 테이블
    private RewardSystem rewardSystem;  // 보상 참조
    private float startTime;  // 게임 시작 시간

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // 게임이 실행 중인지 확인
        if (agent.enabled)
        {
            // UI 표시
            UpdateTimer();
            UpdateTimerUI();
            UpdateRewardUI();

            // 게임 종료 조건을 확인
            if (agent.transform.position.y < -10f || IsTimeUp())
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // 게임 시작 시 초기화 작업을 수행
        startTime = Time.time;
        agent.transform.position = respawnPoint.position;
        agent.enabled = true;

        // 새로운 에피소드 시작
        currentEpisode++;
        if (currentEpisode > maxEpisodes)
        {
            // 최대 에피소드 수에 도달하면 게임 종료 또는 다른 처리 수행
            // 예: 학습 결과 저장, 게임 종료 등
        }
    }

    public void EndGame()
    {
        // 게임 종료 시 처리할 작업을 수행

        // 에이전트를 비활성화
        agent.enabled = false;
    }

    private bool IsTimeUp()
    {
        // 게임 시간이 제한 시간을 초과했는지 확인
        return Time.time - startTime >= timeLimit;
    }

    public void RestartGame()
    {
        // 게임 재시작 시 수행할 작업을 구현, 초기화
        StartGame(); // StartGame 메서드를 호출하여 게임을 재시작
    }

    private void InitializeQTable()
    {
        qTable = new Dictionary<StateActionPair, float>();

        // Q 테이블 초기화 작업 수행
        // 각 상태-행동 쌍에 대한 초기 가치 설정
    }

    private float GetQValue(StateActionPair stateActionPair)
    {
        // Q 테이블에서 특정 상태-행동 쌍의 가치를 반환
        // 존재하지 않는 경우 초기 가치를 반환하거나 예외 처리
        return 0; // 임시
    }

    private void UpdateQValue(StateActionPair stateActionPair, float newValue)
    {
        // Q 테이블에서 특정 상태-행동 쌍의 가치를 업데이트
    }

    private void SelectAndPerformAction(State currentState)
    {
        // 현재 상태에서 가장 높은 가치를 가진 행동 선택
        // 선택된 행동을 수행

        // 수행한 행동에 대한 다음 상태 및 보상을 얻음

        // Q 테이블 업데이트
    }

    private float CalculateReward(State currentState, Action performedAction, State nextState)
    {
        // 현재 상태, 수행한 행동, 다음 상태에 대한 보상 계산
        // 보상 값을 반환
        return 0; // 임시
    }

    private void UpdateTimer()
    {
        // 게임 타이머 업데이트
        if (IsTimeUp())
        {
            EndGame();
        }
    }

    private void UpdateTimerUI()
    {
        // UI에 타이머 값을 표시
        float elapsedTime = Time.time - startTime;
        timerText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
        timeLimitText.text = "Time Limit: " + Mathf.FloorToInt(timeLimit).ToString();
    }

    private void UpdateRewardUI()
    {
        // 보상 값을 UI에 할당하여 표시
        rewardText.text = "Current Reward: " + rewardSystem.GetReward().ToString();
    }
}