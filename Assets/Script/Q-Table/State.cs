// ���¸� �����ϴ� State Ŭ���� ����
public class State
{
    public bool isAlive;    // �÷��̾� ���� ����
    public float groundSlope;    // ���� ����

    public State(bool isAlive, float groundSlope)
    {
        this.isAlive = isAlive;
        this.groundSlope = groundSlope;
    }
}