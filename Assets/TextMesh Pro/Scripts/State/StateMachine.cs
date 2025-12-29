

using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class StateMachine
{
    public List<string> debugForStatck = new List<string>();
    public string debugForCurrentState;

    private List<State> states = new List<State>();
    private Stack<State> stack = new Stack<State>();

    private State _currentState;
    private State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            debugForCurrentState = value.ID.ToString();
        }
    }

    public void OnUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute();
        }
    }

    public void Push(int id)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        State next = states.Find(e => Equals(e.ID, id));
        next.OnEnter();

        CurrentState = next;

        stack.Push(next);

        debugForStatck.Add(id.ToString());
    }

    public void Pop()
    {
        var state = stack.Pop();
        state.OnExit();

        if (stack.Count > 0)
        {
            var prev = stack.Peek();
            if (prev != null)
            {
                CurrentState = prev;
                CurrentState.OnEnter();
            }
        }

        debugForStatck.RemoveAt(debugForStatck.Count - 1);
    }

    public void PopAll()
    {
        var state = stack.Pop();
        state.OnExit();

        stack.Clear();

        debugForStatck.Clear();
    }

    public void Change(int id)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        State next = states.Find(e => Equals(e.ID, id));
        next.OnEnter();

        CurrentState = next;
    }

    public void Add(State state)
    {
        states.Add(state);
    }

    public void Remove(State state)
    {
        states.Remove(state);
    }
}