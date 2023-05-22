using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;    // ������Ʈ�� ����
    public Transform respawnPoint;   // ������ ������ ����
    public TMP_Text timerText;       // Ÿ�̸� UI
    public TMP_Text rewardText;      // ���� UI
    public TMP_Text episodeText;    // ���Ǽҵ� ��ȣ�� ǥ���� UI
    public Button speedButton;       // �ӵ� ���� ��ư
    public Collider endCollider;     // ���� ���Ḧ ���� ���ο� �ݶ��̴�
    public int currentEpisode = 0;   // ���� ���Ǽҵ� ��
    public int maxEpisodes = 100;    // �ִ� ���Ǽҵ� ��
    public float episodeDuration = 180f; // ���Ǽҵ� ���� �ð� (�� ����)

    private Dictionary<StateActionPair, float> qTable;  // Q ���̺�
    private RewardSystem rewardSystem;  // ���� ����
    private GameObject tiltedGround;
    private float startTime;  // ���Ǽҵ� ���� �ð�
    private float episodeTime; // ���� ���Ǽҵ� ��� �ð�
    private float gameSpeed = 1f; // ���� �ӵ�

    private void Start()
    {
        StartGame();
        speedButton.onClick.AddListener(ChangeGameSpeed);
        tiltedGround = GameObject.Find("TiltedGround");
    }

    private void Update()
    {
        // ������ ���� ������ Ȯ��
        if (agent.enabled)
        {
            // ���� Ÿ�̸� ������Ʈ
            UpdateTimer();

            // UI ǥ��
            UpdateTimerUI();
            UpdateRewardUI();

            // ���� ���� ������ Ȯ��
            if (endCollider && endCollider.bounds.Contains(agent.transform.position) || IsEpisodeTimeUp())
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // ���� ���� �� �ʱ�ȭ �۾��� ����
        startTime = Time.time;
        episodeTime = 0f;
        agent.transform.position = respawnPoint.position;
        agent.enabled = true;

        // ���ο� ���Ǽҵ� ����
        currentEpisode++;
        if (currentEpisode > maxEpisodes)
        {
            // �ִ� ���Ǽҵ� ���� �����ϸ� ���� ���� �Ǵ� �ٸ� ó�� ����
            // ��: �н� ��� ����, ���� ���� ��
        }

        // UI�� ���Ǽҵ� ��ȣ�� ǥ��
        episodeText.text = "Episode: " + currentEpisode;
    }

    public void EndGame()    // ���� ���� �� ó���� �۾��� ����
    {
        // ������Ʈ�� ��Ȱ��ȭ
        agent.enabled = false;

        // �ð��� 3������ �ʱ�ȭ
        episodeTime = 0f;
        startTime = Time.time;
    }

    private bool IsEpisodeTimeUp()// ���Ǽҵ� �ð��� ������ ���� �ð��� �ʰ��ߴ��� Ȯ��
    {
        return episodeTime >= episodeDuration;
    }

    public void RestartGame()   // ���� ����� �� ������ �۾��� ����, �ʱ�ȭ
    {
        StartGame(); // StartGame �޼��带 ȣ���Ͽ� ������ �����
    }

    private void InitializeQTable() // Q ���̺� �ʱ�ȭ �۾� ����
    {
        qTable = new Dictionary<StateActionPair, float>();
        // �� ����-�ൿ �ֿ� ���� �ʱ� ��ġ ����
    }

    private float GetQValue(StateActionPair stateActionPair)    // Q ���̺��� Ư�� ����-�ൿ ���� ��ġ�� ��ȯ
    {
        if (qTable.ContainsKey(stateActionPair))
        {
            return qTable[stateActionPair];
        }
        else
        {
            return 0f; // �������� ���� ��� �ʱ� ��ġ�� 0�� ��ȯ
        }
    }

    private void UpdateQValue(StateActionPair stateActionPair, float newValue)  // Q ���̺��� Ư�� ����-�ൿ ���� ��ġ�� ������Ʈ
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
        // ���� ���¿��� ���� ���� ��ġ�� ���� �ൿ ����
        Action bestAction = GetBestAction(currentState);

        // ���õ� �ൿ�� ����
        PerformAction(bestAction);

        // ������ �ൿ�� ���� ���� ���� �� ������ ����
        State nextState = GetState();
        float reward = CalculateReward(currentState, bestAction, nextState);

        // Q ���̺� ������Ʈ
        StateActionPair stateActionPair = new StateActionPair(currentState, bestAction);
        UpdateQValue(stateActionPair, reward);
    }
    private Action GetBestAction(State currentState)     // ���� ���¿��� ���� ���� ��ġ�� ���� �ൿ ����
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
        // ���õ� �ൿ�� ����
        agent.PerformAction(action);
    }
    
    private float CalculateReward(State currentState, Action performedAction, State nextState)  // ���� ����, ������ �ൿ, ���� ���¿� ���� ���� ����ϰ� ��ȯ
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

            // ���� ���� -90������ 90�� ���̷� ����
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
    private void UpdateTimer()  // ���Ǽҵ� ��� �ð� ������Ʈ
    {
        episodeTime = Time.time - startTime;
    }

    private void UpdateTimerUI()    // UI�� Ÿ�̸� ���� ǥ��
    {
        float remainingTime = Mathf.Max(episodeDuration - episodeTime, 0f);

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("Time: {0:00}:{1:00} ({2}x)", minutes, seconds, gameSpeed);
    }

    private void UpdateRewardUI()   // ���� ���� UI�� �Ҵ��Ͽ� ǥ��
    {
        rewardText.text = "Current Reward: " + rewardSystem.GetReward().ToString();
    }

    private void ChangeGameSpeed()  // ���� �ӵ� ����
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