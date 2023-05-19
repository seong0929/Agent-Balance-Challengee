using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    private float totalReward;

    private void Start()
    {
        totalReward = 0f;
    }

    public void AddReward(float reward)
    {
        totalReward += reward;
    }

    public float GetReward()
    {
        return totalReward;
    }
}