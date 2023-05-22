using System.Collections.Generic;
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

    public List<Action> GetPossibleActions()
    {
        // ������ ��� �ൿ�� ��ȯ�ϴ� ������ ����
        List<Action> possibleActions = new List<Action>();

        // ���÷� ������ ���ǵ� �׼ǵ��� �߰�
        for (int i = 0; i < 4; i++)
        {
            Action action = Action.GetActionByIndex(i);
            possibleActions.Add(action);
        }

        return possibleActions;
    }
}