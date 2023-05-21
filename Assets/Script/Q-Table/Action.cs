using UnityEngine;

// �ൿ�� �����ϴ� Action Ŭ���� ����
public class Action
{
    public string actionName;   // �ൿ �̸�
    public int Index { get; private set; }  // �ൿ �ε���

    private static Action[] actions = new Action[4];  // ������ ���ǵ� �׼� �迭

    public Action(string actionName)
    {
        this.actionName = actionName;
        Index = -1;  // �ൿ �ε��� �ʱ�ȭ
    }

    // ������ ���ǵ� �׼ǵ��� �ʱ�ȭ
    static Action()
    {
        actions[0] = new Action("MoveForward");
        actions[1] = new Action("MoveBackward");
        actions[2] = new Action("MoveLeft");
        actions[3] = new Action("MoveRight");
    }

    // �־��� �ε����� �ش��ϴ� �׼� ��ȯ
    public static Action GetActionByIndex(int index)
    {
        if (index >= 0 && index < actions.Length)
        {
            return actions[index];
        }
        else
        {
            Debug.LogError("Invalid action index: " + index);
            return null;
        }
    }
}