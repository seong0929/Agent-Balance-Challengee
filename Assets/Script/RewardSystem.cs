using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    // 보상 값들을 설정합니다.
    public float fallingReward = -1f;  // 떨어질 때의 보상
    public float defaultReward = 0.1f; // 기본 보상

    // 보상을 계산하는 함수입니다.
    public float GetReward(bool isFalling)
    {
        return isFalling ? fallingReward : defaultReward;
    }
}