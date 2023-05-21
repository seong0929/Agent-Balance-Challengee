using UnityEngine;

// 행동을 포함하는 Action 클래스 정의
public class Action
{
    public string actionName;   // 행동 이름
    public int Index { get; private set; }  // 행동 인덱스

    private static Action[] actions = new Action[4];  // 사전에 정의된 액션 배열

    public Action(string actionName)
    {
        this.actionName = actionName;
        Index = -1;  // 행동 인덱스 초기화
    }

    // 사전에 정의된 액션들을 초기화
    static Action()
    {
        actions[0] = new Action("MoveForward");
        actions[1] = new Action("MoveBackward");
        actions[2] = new Action("MoveLeft");
        actions[3] = new Action("MoveRight");
    }

    // 주어진 인덱스에 해당하는 액션 반환
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