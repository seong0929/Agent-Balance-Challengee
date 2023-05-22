using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;    // 에이전트를 참조
    public Transform respawnPoint;   // 리스폰 지점을 설정
    public TMP_Text timerText;       // 타이머 UI
    public TMP_Text rewardText;      // 보상 UI
    public TMP_Text episodeText;    // 에피소드 번호를 표시할 UI
    public Button speedButton;       // 속도 변경 버튼
    public Collider endCollider;     // 게임 종료를 위한 새로운 콜라이더
    public int currentEpisode = 0;   // 현재 에피소드 수
    public int maxEpisodes = 100;    // 최대 에피소드 수
    public float episodeDuration = 180f; // 에피소드 지속 시간 (초 단위)

    private Dictionary<StateActionPair, float> qTable;  // Q 테이블
    private RewardSystem rewardSystem;  // 보상 참조
    private GameObject tiltedGround;
    private float startTime;  // 에피소드 시작 시간
    private float episodeTime; // 현재 에피소드 경과 시간
    private float gameSpeed = 1f; // 게임 속도

    private void Start()
    {
        StartGame();
        speedButton.onClick.AddListener(ChangeGameSpeed);
        tiltedGround = GameObject.Find("TiltedGround");
    }

    private void Update()
    {
        // 게임이 실행 중인지 확인
        if (agent.enabled)
        {
            // 게임 타이머 업데이트
            UpdateTimer();

            // UI 표시
            UpdateTimerUI();
            UpdateRewardUI();

            // 게임 종료 조건을 확인
            if (endCollider && endCollider.bounds.Contains(agent.transform.position) || IsEpisodeTimeUp())
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // 게임 시작 시 초기화 작업을 수행
        startTime = Time.time;
        episodeTime = 0f;
        agent.transform.position = respawnPoint.position;
        agent.enabled = true;

        // 새로운 에피소드 시작
        currentEpisode++;
        if (currentEpisode > maxEpisodes)
        {
            // 최대 에피소드 수에 도달하면 게임 종료 또는 다른 처리 수행
            // 예: 학습 결과 저장, 게임 종료 등
        }

        // UI에 에피소드 번호를 표시
        episodeText.text = "Episode: " + currentEpisode;
    }

    public void EndGame()    // 게임 종료 시 처리할 작업을 수행
    {
        // 에이전트를 비활성화
        agent.enabled = false;

        // 시간을 3분으로 초기화
        episodeTime = 0f;
        startTime = Time.time;
    }

    private bool IsEpisodeTimeUp()// 에피소드 시간이 지정된 지속 시간을 초과했는지 확인
    {
        return episodeTime >= episodeDuration;
    }

    public void RestartGame()   // 게임 재시작 시 수행할 작업을 구현, 초기화
    {
        StartGame(); // StartGame 메서드를 호출하여 게임을 재시작
    }

    private void InitializeQTable() // Q 테이블 초기화 작업 수행
    {
        qTable = new Dictionary<StateActionPair, float>();
        // 각 상태-행동 쌍에 대한 초기 가치 설정
    }

    private float GetQValue(StateActionPair stateActionPair)    // Q 테이블에서 특정 상태-행동 쌍의 가치를 반환
    {
        if (qTable.ContainsKey(stateActionPair))
        {
            return qTable[stateActionPair];
        }
        else
        {
            return 0f; // 존재하지 않을 경우 초기 가치인 0을 반환
        }
    }

    private void UpdateQValue(StateActionPair stateActionPair, float newValue)  // Q 테이블에서 특정 상태-행동 쌍의 가치를 업데이트
    {
        if (qTable.ContainsKey(stateActionPair))
        {
            qTable[stateActionPair] = newValue;
        }
        else
        {
            qTable.Add(stateActionPair, newValue);
        }
    }

    private void SelectAndPerformAction(State currentState)
    {
        // 현재 상태에서 가장 높은 가치를 가진 행동 선택
        Action bestAction = GetBestAction(currentState);

        // 선택된 행동을 수행
        PerformAction(bestAction);

        // 수행한 행동에 대한 다음 상태 및 보상을 얻음
        State nextState = GetState();
        float reward = CalculateReward(currentState, bestAction, nextState);

        // Q 테이블 업데이트
        StateActionPair stateActionPair = new StateActionPair(currentState, bestAction);
        UpdateQValue(stateActionPair, reward);
    }
    private Action GetBestAction(State currentState)     // 현재 상태에서 가장 높은 가치를 가진 행동 선택
    {
        Action bestAction = null;
        float bestQValue = float.MinValue;

        foreach (Action action in currentState.GetPossibleActions())
        {
            StateActionPair stateActionPair = new StateActionPair(currentState, action);
            float qValue = GetQValue(stateActionPair);

            if (qValue > bestQValue)
            {
                bestQValue = qValue;
                bestAction = action;
            }
        }

        return bestAction;
    }

    private void PerformAction(Action action)
    {
        // 선택된 행동을 수행
        agent.PerformAction(action);
    }
    
    private float CalculateReward(State currentState, Action performedAction, State nextState)  // 현재 상태, 수행한 행동, 다음 상태에 대한 보상 계산하고 반환
    {
        float reward = rewardSystem.CalculateReward(currentState, performedAction, nextState);
        return reward;
    }
    private bool GetPlayerSurvivalStatus()
    {
        return agent.enabled;
    }

    private float GetGroundSlope()
    {
        if (tiltedGround != null)
        {
            float slope = tiltedGround.transform.rotation.eulerAngles.x;

            // 기울기 값을 -90도에서 90도 사이로 조정
            slope = Mathf.Clamp(slope, -90f, 90f);

            return slope;
        }
        else
        {
            Debug.LogError("Tilted ground object is not assigned!");
            return 0f;
        }
    }

    private State GetState()
    {
        bool isAlive = GetPlayerSurvivalStatus();
        float groundSlope = GetGroundSlope();

        State currentState = new State(isAlive, groundSlope);
        return currentState;
    }
    private void UpdateTimer()  // 에피소드 경과 시간 업데이트
    {
        episodeTime = Time.time - startTime;
    }

    private void UpdateTimerUI()    // UI에 타이머 값을 표시
    {
        float remainingTime = Mathf.Max(episodeDuration - episodeTime, 0f);

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("Time: {0:00}:{1:00} ({2}x)", minutes, seconds, gameSpeed);
    }

    private void UpdateRewardUI()   // 보상 값을 UI에 할당하여 표시
    {
        rewardText.text = "Current Reward: " + rewardSystem.GetReward().ToString();
    }

    private void ChangeGameSpeed()  // 게임 속도 변경
    {
        if (gameSpeed == 1f)
        {
            gameSpeed = 2f;
            Time.timeScale = gameSpeed;
        }
        else if (gameSpeed == 2f)
        {
            gameSpeed = 4f;
            Time.timeScale = gameSpeed;
        }
        else
        {
            gameSpeed = 1f;
            Time.timeScale = gameSpeed;
        }
    }
}