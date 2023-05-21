// 상태를 포함하는 State 클래스 정의
public class State
{
    public bool isAlive;    // 플레이어 생존 여부
    public float groundSlope;    // 땅의 기울기

    public State(bool isAlive, float groundSlope)
    {
        this.isAlive = isAlive;
        this.groundSlope = groundSlope;
    }
}