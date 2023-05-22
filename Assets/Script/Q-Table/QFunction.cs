using System.Collections.Generic;
using UnityEngine;

namespace QFunction
{
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
}