using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : Character
{
    public NPC Target { get; private set; }
    
    //public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake()
    {
        InitializeStateMachine();
        SetCellType(_cellType);
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>
        {
            {typeof(PatrolState), new PatrolState(this) },
            {typeof(ChaseState), new ChaseState(this) },
        };
        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(NPC target)
    {
        if(target._cellType != _cellType) Target = target;
    }
}
