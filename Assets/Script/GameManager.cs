using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;   // ������Ʈ�� ����
    public Transform respawnPoint;  // ������ ������ ����
    public TMP_Text timerText;      // Ÿ�̸� UI
    public TMP_Text rewardText; // ���� UI
    public int currentEpisode = 0;  // ���� ���Ǽҵ� ��
    public int maxEpisodes = 100;  // �ִ� ���Ǽҵ� ��

    private Dictionary<StateActionPair, float> qTable;  // Q ���̺�
    private RewardSystem rewardSystem;  // ���� ����
    private float timer = 0f;       // ���� Ÿ�̸Ӹ� ����

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // ������ ���� ������ Ȯ��
        if (agent.enabled)
        {
            timer += Time.deltaTime;

            // UI ǥ��
            UpdateTimerUI();
            UpdateRewardUI();
            // ���� ���� ������ Ȯ��
            if (agent.transform.position.y < -10f)  // ������Ʈ�� ������ ������ �Ʒ��� ���� ��
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // ���� ���� �� �ʱ�ȭ �۾��� ����
        timer = 0f;
        agent.transform.position = respawnPoint.position;
        agent.enabled = true;

        // ���ο� ���Ǽҵ� ����
        currentEpisode++;
        if (currentEpisode > maxEpisodes)
        {
            // �ִ� ���Ǽҵ� ���� �����ϸ� ���� ���� �Ǵ� �ٸ� ó�� ����
            // ��: �н� ��� ����, ���� ���� ��
        }
    }

    public void EndGame()
    {
        // ���� ���� �� ó���� �۾��� ����

        // ������Ʈ�� ��Ȱ��ȭ
        agent.enabled = false;
    }
    public void RestartGame()
    {
        // ���� ����� �� ������ �۾��� ����, �ʱ�ȭ
        StartGame(); // StartGame �޼��带 ȣ���Ͽ� ������ �����
    }
    private void InitializeQTable()
    {
        qTable = new Dictionary<StateActionPair, float>();

        // Q ���̺� �ʱ�ȭ �۾� ����
        // �� ����-�ൿ �ֿ� ���� �ʱ� ��ġ ����
    }

    private float GetQValue(StateActionPair stateActionPair)
    {
        // Q ���̺��� Ư�� ����-�ൿ ���� ��ġ�� ��ȯ
        // �������� �ʴ� ��� �ʱ� ��ġ�� ��ȯ�ϰų� ���� ó��
        return 0; //�ӽ�
    }

    private void UpdateQValue(StateActionPair stateActionPair, float newValue)
    {
        // Q ���̺��� Ư�� ����-�ൿ ���� ��ġ�� ������Ʈ
    }
    private void SelectAndPerformAction(State currentState)
    {
        // ���� ���¿��� ���� ���� ��ġ�� ���� �ൿ ����
        // ���õ� �ൿ�� ����

        // ������ �ൿ�� ���� ���� ���� �� ������ ����

        // Q ���̺� ������Ʈ
    }
    private float CalculateReward(State currentState, Action performedAction, State nextState)
    {
        // ���� ����, ������ �ൿ, ���� ���¿� ���� ���� ���
        // ���� ���� ��ȯ
        return 0; //�ӽ�
    }
    private void UpdateTimerUI()
    {
        // UI�� Ÿ�̸� ���� ǥ��
        timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
    }
    private void UpdateRewardUI()
    {
        // ���� ���� UI�� �Ҵ��Ͽ� ǥ��
        rewardText.text = "Current Reward: " + rewardSystem.GetReward().ToString();
    }
}