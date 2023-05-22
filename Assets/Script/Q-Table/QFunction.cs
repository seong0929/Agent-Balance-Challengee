using System.Collections.Generic;
using UnityEngine;

namespace QFunction
{
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
}