using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject _gameObject;
    protected Transform _transform;

    public BaseState(GameObject gameObject)
    {
        this._gameObject = gameObject;
        this._transform = gameObject.transform;
    }
    public abstract Type Tick();
}
