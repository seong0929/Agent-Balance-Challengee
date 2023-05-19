using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    // ���� ������ �����մϴ�.
    public float fallingReward = -1f;  // ������ ���� ����
    public float defaultReward = 0.1f; // �⺻ ����

    // ������ ����ϴ� �Լ��Դϴ�.
    public float GetReward(bool isFalling)
    {
        return isFalling ? fallingReward : defaultReward;
    }
}