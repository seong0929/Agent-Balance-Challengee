public class QTable
{
    private float[,] qTable;

    public QTable(int stateCount, int actionCount)
    {
        qTable = new float[stateCount, actionCount];

        // Q-Table�� �ʱⰪ���� ����
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
        // ������ �ൿ�� �����Ͽ� ��ȯ�մϴ�.
        // Q-Table�� �����Ͽ� ������ �ൿ�� �����ؾ� �մϴ�.
        return 0; // ������ �ൿ�� �ش��ϴ� ���� ��ȯ�մϴ�.
    }

    public void UpdateQValue(int state, int action, int nextState, float reward)
    {
        // Q-Table�� ���� ������Ʈ�մϴ�.
        // ������Ʈ ��Ģ�� ���� Q-Table ���� �����ؾ� �մϴ�.
    }
}
// ���¿� �ൿ�� �����ϴ� StateActionPair Ŭ���� ����
public class StateActionPair
{
    public State state;   // ����
    public Action action; // �ൿ

    public StateActionPair(State state, Action action)
    {
        this.state = state;
        this.action = action;
    }
}

// ���¸� �����ϴ� State Ŭ���� ����
public class State
{
    public bool isAlive;   // �÷��̾� ���� ����

    public State(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}

// �ൿ�� �����ϴ� Action Ŭ���� ����
public class Action
{
    public string actionName;   // �ൿ �̸�

    public Action(string actionName)
    {
        this.actionName = actionName;
    }

    // ������ ���ǵ� �׼�
    public static Action MoveForward = new Action("MoveForward");  // ������ �̵�
    public static Action MoveBackward = new Action("MoveBackward");  // �ڷ� �̵�
    public static Action MoveLeft = new Action("MoveLeft");  // �������� �̵�
    public static Action MoveRight = new Action("MoveRight");  // ���������� �̵�
}