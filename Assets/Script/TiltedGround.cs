using UnityEngine;

public class TiltedGround : MonoBehaviour
{
    public float tiltSpeed = 10f;          // 땅의 기울기 변화 속도
    public float tiltAmount = 10f;         // 땅의 최대 기울기 각도
    public float timeToNextTilt = 10f;     // 다음 기울기로 변경되기까지의 시간

    private float timeOffset;              // 시작 시간과의 차이를 저장하는 변수
    private Quaternion initialRotation;     // 초기 회전값
    private Quaternion targetRotation;      // 목표 회전값

    private void Start()
    {
        timeOffset = Time.time; // 시작 시간과의 차이를 계산하여 저장
        initialRotation = transform.rotation; // 초기 회전값 저장
        targetRotation = GetRandomRotation(); // 다음 목표 회전값 랜덤하게 설정
    }

    private void Update()
    {
        float elapsedTime = Time.time - timeOffset;  // 경과 시간 계산

        if (elapsedTime >= timeToNextTilt)
        {
            timeOffset = Time.time; // 시작 시간 갱신
            initialRotation = transform.rotation; // 현재 회전값을 초기 회전값으로 설정
            targetRotation = GetRandomRotation(); // 다음 목표 회전값 랜덤하게 설정
        }

        float t = elapsedTime / timeToNextTilt;  // 경과 시간의 비율 계산
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
    }

    private Quaternion GetRandomRotation()
    {
        float randomTiltX = Random.Range(-tiltAmount, tiltAmount);
        float randomTiltZ = Random.Range(-tiltAmount, tiltAmount);
        return Quaternion.Euler(randomTiltX, 0f, randomTiltZ);
    }
}