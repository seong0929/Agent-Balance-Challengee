using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AgentController agent;   // AgentController ��ũ��Ʈ�� ���� ������Ʈ�� �����մϴ�.
    public Transform respawnPoint;  // ������ ������ �����մϴ�.
    public TMP_Text timerText;      // UI�� Ÿ�̸Ӹ� ǥ���� TMP_Text ������Ʈ�� �����մϴ�.

    private float timer = 0f;       // ���� Ÿ�̸Ӹ� �����մϴ�.

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // ������ ���� ������ Ȯ���մϴ�.
        if (agent.enabled)
        {
            timer += Time.deltaTime;

            // Ÿ�̸� ���� UI�� ǥ���մϴ�.
            UpdateTimerUI();

            // ���� ���� ������ Ȯ���մϴ�.
            if (agent.transform.position.y < -10f)  // ������Ʈ�� ������ ������ �Ʒ��� ���� ��
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // ���� ���� �� �ʱ�ȭ �۾��� �����մϴ�.
        timer = 0f;

        // ������Ʈ�� ������ �������� �̵���ŵ�ϴ�.
        agent.transform.position = respawnPoint.position;

        // ������Ʈ�� Ȱ��ȭ�մϴ�.
        agent.enabled = true;
    }

    public void EndGame()
    {
        // ���� ���� �� ó���� �۾��� �����մϴ�.

        // ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        agent.enabled = false;

        // ���� ���ῡ ���� �߰� ������ �����մϴ�.

        // ���� ���, ���� ������ ����ϰų� ����� ǥ���� �� �ֽ��ϴ�.
    }

    private void UpdateTimerUI()
    {
        // UI�� Ÿ�̸� ���� ǥ���մϴ�.
        timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
    }
}