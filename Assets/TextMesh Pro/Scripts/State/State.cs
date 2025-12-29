

using System.Collections.Generic;
using UnityEngine;

public class State
{
    public State(int id)
    {
        this.ID = id;
    }


    public int ID { get; private set; }

    public System.Action onEnter;
    public System.Action onExecute;
    public System.Action onExit;

    public void OnEnter()
    {
        if (onEnter != null)
        {
            onEnter();
        }
    }

    public void OnExecute()
    {
        if (onExecute != null)
        {
            onExecute();
        }
    }
    
    public void OnExit()
    {
        if (onExit != null)
        {
            onExit();
        }

    }
}