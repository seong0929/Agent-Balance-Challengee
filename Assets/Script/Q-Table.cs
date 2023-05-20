public class QTable
{
    private float[,] qTable;

    public QTable(int stateCount, int actionCount)
    {
        qTable = new float[stateCount, actionCount];

        // Q-Table을 초기값으로 설정
        for (int i = 0; i < stateCount; i++)
        {
            for (int j = 0; j < actionCount; j++)
            {
                qTable[i, j] = 0f;
            }
        }
    }

    public int GetBestAction(int state)
    {
        // 최적의 행동을 결정하여 반환합니다.
        // Q-Table을 참고하여 최적의 행동을 선택해야 합니다.
        return 0; // 최적의 행동에 해당하는 값을 반환합니다.
    }

    public void UpdateQValue(int state, int action, int nextState, float reward)
    {
        // Q-Table의 값을 업데이트합니다.
        // 업데이트 규칙에 따라 Q-Table 값을 수정해야 합니다.
    }
}
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

// 상태를 포함하는 State 클래스 정의
public class State
{
    public bool isAlive;   // 플레이어 생존 여부

    public State(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}

// 행동을 포함하는 Action 클래스 정의
public class Action
{
    public string actionName;   // 행동 이름

    public Action(string actionName)
    {
        this.actionName = actionName;
    }

    // 사전에 정의된 액션
    public static Action MoveForward = new Action("MoveForward");  // 앞으로 이동
    public static Action MoveBackward = new Action("MoveBackward");  // 뒤로 이동
    public static Action MoveLeft = new Action("MoveLeft");  // 왼쪽으로 이동
    public static Action MoveRight = new Action("MoveRight");  // 오른쪽으로 이동
}