using UnityEngine;

public class TiltedGround : MonoBehaviour
{
    public float tiltSpeed = 10f;          // ���� ���� ��ȭ �ӵ�
    public float tiltAmount = 10f;         // ���� �ִ� ���� ����
    public float timeToNextTilt = 10f;     // ���� ����� ����Ǳ������ �ð�

    private float timeOffset;              // ���� �ð����� ���̸� �����ϴ� ����
    private Quaternion initialRotation;     // �ʱ� ȸ����
    private Quaternion targetRotation;      // ��ǥ ȸ����

    private void Start()
    {
        timeOffset = Time.time; // ���� �ð����� ���̸� ����Ͽ� ����
        initialRotation = transform.rotation; // �ʱ� ȸ���� ����
        targetRotation = GetRandomRotation(); // ���� ��ǥ ȸ���� �����ϰ� ����
    }

    private void Update()
    {
        float elapsedTime = Time.time - timeOffset;  // ��� �ð� ���

        if (elapsedTime >= timeToNextTilt)
        {
            timeOffset = Time.time; // ���� �ð� ����
            initialRotation = transform.rotation; // ���� ȸ������ �ʱ� ȸ�������� ����
            targetRotation = GetRandomRotation(); // ���� ��ǥ ȸ���� �����ϰ� ����
        }

        float t = elapsedTime / timeToNextTilt;  // ��� �ð��� ���� ���
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
    }

    private Quaternion GetRandomRotation()
    {
        float randomTiltX = Random.Range(-tiltAmount, tiltAmount);
        float randomTiltZ = Random.Range(-tiltAmount, tiltAmount);
        return Quaternion.Euler(randomTiltX, 0f, randomTiltZ);
    }
}