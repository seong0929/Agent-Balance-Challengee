// 상태와 행동을 포함하는 StateActionPair 클래스 정의
public class StateActionPair
{
    public State state;   // 상태
    public Action action; // 행동

    public StateActionPair(State state, Action action)
    {
        this.state = state;
        this.action = action;
    }
}