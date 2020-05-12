using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private NPC _npc;

    public ChaseState(NPC npc) : base(npc.gameObject)
    {
        _npc = npc;
    }

    public override Type Tick()
    {
        if(_npc.Target == null && _npc.Target._cellType == _npc._cellType)
        {
            return typeof(PatrolState);
        }

        _transform.LookAt(_npc.Target.transform);
        _transform.Translate(Vector3.forward * Time.deltaTime * (GameSettings.NPCSpeed + 2f));
        
        var distance = Vector3.Distance(_transform.position, _npc.Target.transform.position);
        if(distance <= GameSettings.TurnRadius)
        {
            return typeof(PatrolState);
        }
        return typeof(PatrolState);
    }


}
