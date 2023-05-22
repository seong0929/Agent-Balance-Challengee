using System.Collections.Generic;
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

    public List<Action> GetPossibleActions()
    {
        // 가능한 모든 행동을 반환하는 로직을 구현
        List<Action> possibleActions = new List<Action>();

        // 예시로 사전에 정의된 액션들을 추가
        for (int i = 0; i < 4; i++)
        {
            Action action = Action.GetActionByIndex(i);
            possibleActions.Add(action);
        }

        return possibleActions;
    }
}