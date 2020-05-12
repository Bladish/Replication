using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private Vector3? _destination;
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private NPC _npc;

    public PatrolState(NPC npc) : base(npc.gameObject)
    {
        _npc = npc;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null)
        {
            _npc.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        if(_destination.HasValue == false || Vector3.Distance(_transform.position, _destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        _transform.rotation = Quaternion.Slerp(_transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);

        if (IsForwardBlocked())
        {
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _desiredRotation, 0.2f);
        }
        else
        {
            _transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.NPCSpeed);
        }

        if (IsPathBlocked())
        {
            FindRandomDestination();
        }
        return null;
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(_transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, GameSettings.RayDistance, _layerMask);
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(_transform.position, _transform.forward);
        return Physics.SphereCast(ray, 0.5f, GameSettings.RayDistance, _layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (_transform.position + (_transform.forward * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-4.5f, 4.5f), UnityEngine.Random.Range(-4.5f, 4.5f));
        _destination = new Vector3(testPosition.x, testPosition.y, testPosition.z);
        _direction = Vector3.Normalize(_destination.Value - _transform.position);
        _direction = new Vector3(_direction.x, _direction.y, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    
    private NPC CheckForAggro()
    {
        Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
        RaycastHit hit;
        var angle = _transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = _transform.position;
        for(int i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, GameSettings.AggroRadius))
            {
                var npc = hit.collider.GetComponent<NPC>();
                if(npc != null && npc._cellType != _gameObject.GetComponent<NPC>()._cellType)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    switch (_npc._cellType)
                    {   
                        case CellType.Virus:
                            if (npc._cellType == CellType.RedBloodCell) return npc;
                            if (npc._cellType == CellType.WhiteBloodCell) direction = stepAngle * direction;
                            break;
                        case CellType.WhiteBloodCell:
                            if (npc._cellType == CellType.Virus) return npc;
                            break;
                        case CellType.RedBloodCell:
                            if (npc._cellType == CellType.Virus) direction = stepAngle * direction;
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
                {
                    Debug.DrawRay(pos, direction * GameSettings.AggroRadius, Color.white);
                }
            direction = stepAngle * direction;
            
        }
        return null;
    }
}
