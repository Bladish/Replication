using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates;
    
    public BaseState CurrentState { get; set; }

    public event Action<BaseState> OnStateChange;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }
   
    // Update is called once per frame
    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();
    
        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    public void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState];
        OnStateChange?.Invoke(CurrentState);
    }
}
